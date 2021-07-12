using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ZcraPortal.Data;
using ZcraPortal.Dtos;
using ZcraPortal.Model;

namespace ZcraPortal.Controllers
{

    [Route("api/Quickaccesstools")]
    [ApiController]
    public class QuickAccessToolsController : ControllerBase
    {
        private readonly IZcraPortalRepo _repository;
        private IMapper _mapper;
        private IConfiguration _configuration;

        public QuickAccessToolsController(IZcraPortalRepo repository, IMapper mapper, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult<IEnumerable<QuickAccessToolReadDto>> GetAllQuickAccessTools()
        {
            var allQuickAccessTools = _repository.GetAll<Quickaccesstools>();
            return Ok(_mapper.Map<IEnumerable<QuickAccessToolReadDto>>(allQuickAccessTools));
        }

        [HttpGet("{id}", Name = "GetFirstQuickAccessTool")]
        public ActionResult<QuickAccessToolReadDto> GetFirstQuickAccessTool(int id)
        {
            var theQuickAccessTool = _repository.GetFirst<Quickaccesstools>(x => x.Id == id);
            if (theQuickAccessTool != null)
            {
                return Ok(_mapper.Map<QuickAccessToolReadDto>(theQuickAccessTool));
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPost]
        public ActionResult<QuickAccessToolReadDto> CreateQuickAccessTool(QuickAccessToolCreateDto QuickAccessToolCreateDto)
        {
            var QuickAccessToolModel = _mapper.Map<Quickaccesstools>(QuickAccessToolCreateDto);
            _repository.Create(QuickAccessToolModel);
            _repository.SaveChanges();

            var QuickAccessToolReadDto = _mapper.Map<QuickAccessToolReadDto>(QuickAccessToolModel);

            return CreatedAtRoute(nameof(GetFirstQuickAccessTool), new { id = QuickAccessToolReadDto.Id }, QuickAccessToolReadDto);
        }

        [Route("upload")]
        [HttpPost]
        public async Task<IActionResult> OnPostUploadAsync([FromForm] UploadFile uploadFile)
        {
            try
            {
                var uploadFolder = "Quick_Access_Tools";
                var theFile = uploadFile.file;
                string xampFolderUrl = _configuration["XampFolderUrl"].ToString();

                var uploadPath = Path.Combine(xampFolderUrl, uploadFolder);

                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                var filePath = Path.Combine(uploadPath, theFile.FileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    await theFile.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.ToString());
            }

            return Ok();
        }


        [HttpPut("{id}")]
        public ActionResult<QuickAccessToolUpdateDto> UpdateQuickAccessTool(QuickAccessToolUpdateDto QuickAccessToolUpdateDto, int id)
        {
            var QuickAccessToolFromRepo = _repository.GetFirst<Quickaccesstools>(x => x.Id == id);
            if (QuickAccessToolFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(QuickAccessToolUpdateDto, QuickAccessToolFromRepo);

            _repository.Update(QuickAccessToolFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult PartialUpdateQuickAccessTool(int id, JsonPatchDocument<QuickAccessToolUpdateDto> jsonPatchDocument)
        {
            var QuickAccessToolFromRepo = _repository.GetFirst<Quickaccesstools>(x => x.Id == id);
            if (QuickAccessToolFromRepo == null)
            {
                return NotFound();
            }

            var QuickAccessToolToPatch = _mapper.Map<QuickAccessToolUpdateDto>(QuickAccessToolFromRepo);
            jsonPatchDocument.ApplyTo(QuickAccessToolToPatch, ModelState);
            if (!TryValidateModel(QuickAccessToolToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(QuickAccessToolToPatch, QuickAccessToolFromRepo);
            _repository.Update(QuickAccessToolFromRepo);
            _repository.SaveChanges();

            return NoContent();

        }

        [HttpDelete("{id}")]
        public ActionResult DeleteQuickAccessTool(int id)
        {
            var QuickAccessToolFromRepo = _repository.GetFirst<Quickaccesstools>(x => x.Id == id);
            if (QuickAccessToolFromRepo == null)
            {
                return NotFound();
            }

            _repository.Delete(QuickAccessToolFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}