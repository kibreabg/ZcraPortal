
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZcraPortal.Model;

namespace ZcraPortal.Model
{
    public class UserWithToken : Users
    {
        
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public UserWithToken(Users user)
        {
            this.Id = user.Id;
            this.Username = user.Username;            
            this.Email = user.Email;
            this.Password = user.Password;
            this.RememberToken = user.RememberToken;
            this.CreatedAt = user.CreatedAt;
            this.UpdatedAt = user.UpdatedAt;
        }
    }
}