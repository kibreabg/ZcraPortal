using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ZcraPortal.Data;
using ZcraPortal.Dtos;
using ZcraPortal.Model;

namespace ZcraPortal.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase {
        private readonly IZcraPortalRepo _repository;
        private readonly JWTSettings _jwtsettings;
        private readonly IMapper _mapper;
        private string developmentString = "http://localhost:5000/api/Users/Login";
        private string productionString = "http://portal.zcra.com/zcrabackend/api/Users/Login";

        public UsersController (IZcraPortalRepo repository, IOptions<JWTSettings> jwtsettings, IMapper mapper) {
            _repository = repository;
            _jwtsettings = jwtsettings.Value;
            _mapper = mapper;
        }

        // GET: api/Users
        [HttpGet]
        public ActionResult<IEnumerable<Users>> GetUsers () {
            var allUsers = _repository.GetAll<Users> ();
            return Ok (allUsers);
        }

        // GET: api/Users/5
        [HttpGet ("{id}", Name = "GetUser")]
        public ActionResult<Users> GetUser (int id) {
            var theUser = _repository.GetFirst<Users> (x => x.Id == id);
            if (theUser != null) {
                return Ok (theUser);
            } else {
                return NotFound ();
            }
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut ("{id}")]
        public ActionResult<UserUpdateDto> UpdateUser (int id, UserUpdateDto userUpdateDto) {
            var userFromRepo = _repository.GetFirst<Users> (x => x.Id == id);
            if (userFromRepo == null) {
                return NotFound ();
            }
            _mapper.Map (userUpdateDto, userFromRepo);

            _repository.Update<Users> (userFromRepo);
            _repository.SaveChanges ();

            return NoContent ();
        }

        // POST: api/Users
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public ActionResult<UserReadDto> CreateUser (UserCreateDto userCreateDto) {
            var userModel = _mapper.Map<Users> (userCreateDto);
            _repository.Create<Users> (userModel);
            _repository.SaveChanges ();

            var userReadDto = _mapper.Map<UserReadDto> (userModel);

            return CreatedAtRoute (nameof (GetUser), new { id = userReadDto.Id }, userReadDto);
        }

        // DELETE: api/Users/5
        [HttpDelete ("{id}")]
        public ActionResult<Users> DeleteUser (int id) {
            var userFromRepo = _repository.GetFirst<Users> (x => x.Id == id);
            if (userFromRepo == null) {
                return NotFound ();
            }

            _repository.Delete<Users> (userFromRepo);
            _repository.SaveChanges ();

            return NoContent ();
        }

        private bool UserExists (int id) {
            var userFromRepo = _repository.GetFirst<Users> (x => x.Id == id);
            if (userFromRepo == null) {
                return false;
            } else
                return true;
        }

        // POST: api/Users
        [HttpPost ("Login")]
        public ActionResult<UserWithToken> Login ([FromBody] Users user) {
            user = _repository.GetFirst<Users> (x => x.Username == user.Username && x.Password == user.Password);
            UserWithToken userWithToken = null;

            if (user != null) {
                RefreshToken refreshToken = GenerateRefreshToken ();

                user.RefreshTokens.Add (refreshToken);
                _repository.SaveChanges ();

                userWithToken = new UserWithToken (user);
                userWithToken.RefreshToken = refreshToken.Token;
            }

            if (userWithToken == null) {
                return NotFound ();
            }

            //sign your token here..
            userWithToken.AccessToken = GenerateAccessToken (user.Id);
            return userWithToken;
        }

        // POST: api/Users
        [HttpPost ("RegisterUser")]
        public ActionResult<UserWithToken> RegisterUser ([FromBody] Users user) {

            _repository.Create<Users> (user);
            _repository.SaveChanges ();

            UserWithToken userWithToken = null;

            if (user != null) {
                RefreshToken refreshToken = GenerateRefreshToken ();
                user.RefreshTokens.Add (refreshToken);
                _repository.SaveChanges ();

                userWithToken = new UserWithToken (user);
                userWithToken.RefreshToken = refreshToken.Token;
            }

            if (userWithToken == null) {
                return NotFound ();
            }

            //sign your token here here..
            userWithToken.AccessToken = GenerateAccessToken (user.Id);
            return userWithToken;
        }

        // GET: api/Users
        [HttpPost ("RefreshToken")]
        public ActionResult<UserWithToken> RefreshToken ([FromBody] RefreshRequest refreshRequest) {
            Users user = GetUserFromAccessToken (refreshRequest.AccessToken);

            if (user != null && ValidateRefreshToken (user, refreshRequest.RefreshToken)) {
                UserWithToken userWithToken = new UserWithToken (user);
                userWithToken.AccessToken = GenerateAccessToken (user.Id);

                return userWithToken;
            }

            return null;
        }

        // GET: api/Users
        [HttpPost ("GetUserByAccessToken")]
        public ActionResult<Users> GetUserByAccessToken ([FromBody] string accessToken) {
            Users user = GetUserFromAccessToken (accessToken);

            if (user != null) {
                return user;
            }

            return null;
        }

        private bool ValidateRefreshToken (Users user, string refreshToken) {

            RefreshToken refreshTokenUser = _repository.GetFirst<RefreshToken> (x => x.Token == refreshToken);

            if (refreshTokenUser != null && refreshTokenUser.UserId == user.Id &&
                refreshTokenUser.ExpiryDate > DateTime.UtcNow) {
                return true;
            }

            return false;
        }

        private Users GetUserFromAccessToken (string accessToken) {
            try {
                var tokenHandler = new JwtSecurityTokenHandler ();
                var key = Encoding.ASCII.GetBytes (_jwtsettings.SecretKey);

                var tokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey (key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                SecurityToken securityToken;
                var principle = tokenHandler.ValidateToken (accessToken, tokenValidationParameters, out securityToken);

                JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;

                if (jwtSecurityToken != null && jwtSecurityToken.Header.Alg.Equals (SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase)) {
                    var userId = principle.FindFirst (ClaimTypes.Name)?.Value;

                    return _repository.GetFirst<Users> (x => x.Id == Convert.ToInt32 (userId));
                }
            } catch (Exception) {
                return new Users ();
            }

            return new Users ();
        }

        private RefreshToken GenerateRefreshToken () {
            RefreshToken refreshToken = new RefreshToken ();

            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create ()) {
                rng.GetBytes (randomNumber);
                refreshToken.Token = Convert.ToBase64String (randomNumber);
            }
            refreshToken.ExpiryDate = DateTime.UtcNow.AddMonths (6);

            return refreshToken;
        }

        private string GenerateAccessToken (int userId) {
            var tokenHandler = new JwtSecurityTokenHandler ();
            var key = Encoding.ASCII.GetBytes (_jwtsettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity (new Claim[] {
                new Claim (ClaimTypes.Name, Convert.ToString (userId))
                }),
                Issuer = developmentString,
                Expires = DateTime.UtcNow.AddDays (1),
                SigningCredentials = new SigningCredentials (new SymmetricSecurityKey (key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken (tokenDescriptor);
            return tokenHandler.WriteToken (token);
        }
    }
}