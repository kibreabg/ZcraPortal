using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ZcraPortal.Data;
using ZcraPortal.Dtos;
using ZcraPortal.Model;

namespace ZcraPortal.Controllers
{

    [Route("api/guidelines")]
    [ApiController]
    public class GuidelinesController : ControllerBase
    {
        private readonly IZcraPortalRepo _repository;
        private IMapper _mapper;

        public GuidelinesController(IZcraPortalRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<GuidelineReadDto>> GetAllGuidelines()
        {
            var allGuidelines = _repository.GetAll<Guidelines>();
            return Ok(_mapper.Map<IEnumerable<GuidelineReadDto>>(allGuidelines));
        }

        [HttpGet("{id}", Name = "GetFirstGuideline")]
        public ActionResult<GuidelineReadDto> GetFirstGuideline(int id)
        {
            var theGuideline = _repository.GetFirst<Guidelines>(x => x.Id == id);
            if (theGuideline != null)
            {
                return Ok(_mapper.Map<GuidelineReadDto>(theGuideline));
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPost]
        public ActionResult<GuidelineReadDto> CreateGuideline(GuidelineCreateDto guidelineCreateDto)
        {
            var guidelineModel = _mapper.Map<Guidelines>(guidelineCreateDto);
            _repository.Create<Guidelines>(guidelineModel);
            _repository.SaveChanges();

            var guidelineReadDto = _mapper.Map<GuidelineReadDto>(guidelineModel);

            return CreatedAtRoute(nameof(GetFirstGuideline), new { id = guidelineReadDto.Id }, guidelineReadDto);
        }

        [Route("upload")]
        [HttpPost]
        public async Task<IActionResult> OnPostUploadAsync([FromForm] UploadFile uploadFile)
        {
            try
            {
                var uploadFolder = uploadFile.gtype;
                var theFile = uploadFile.file;

                var uploadPath = Path.Combine("C:\\xampp\\htdocs\\zhcra", uploadFolder);

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
        public ActionResult<GuidelineUpdateDto> UpdateGuideline(GuidelineUpdateDto guidelineUpdateDto, int id)
        {
            var guidelineFromRepo = _repository.GetFirst<Guidelines>(x => x.Id == id);
            if (guidelineFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(guidelineUpdateDto, guidelineFromRepo);

            _repository.Update<Guidelines>(guidelineFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult PartialUpdateGuideline(int id, JsonPatchDocument<GuidelineUpdateDto> jsonPatchDocument)
        {
            var guidelineFromRepo = _repository.GetFirst<Guidelines>(x => x.Id == id);
            if (guidelineFromRepo == null)
            {
                return NotFound();
            }

            var guidelineToPatch = _mapper.Map<GuidelineUpdateDto>(guidelineFromRepo);
            jsonPatchDocument.ApplyTo(guidelineToPatch, ModelState);
            if (!TryValidateModel(guidelineToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(guidelineToPatch, guidelineFromRepo);
            _repository.Update<Guidelines>(guidelineFromRepo);
            _repository.SaveChanges();

            return NoContent();

        }

        [HttpDelete("{id}")]
        public ActionResult DeleteGuideline(int id)
        {
            var guidelineFromRepo = _repository.GetFirst<Guidelines>(x => x.Id == id);
            if (guidelineFromRepo == null)
            {
                return NotFound();
            }

            _repository.Delete<Guidelines>(guidelineFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}