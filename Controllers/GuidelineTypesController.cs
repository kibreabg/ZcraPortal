using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ZcraPortal.Data;
using ZcraPortal.Dtos;
using ZcraPortal.Model;

namespace ZcraPortal.Controllers {
    [Route ("api/guidelinetypes")]
    [ApiController]
    public class GuidelineTypesController : ControllerBase {
        private readonly IZcraPortalRepo _repository;
        private IMapper _mapper;

        public GuidelineTypesController (IZcraPortalRepo repository, IMapper mapper) {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<GuidelineTypeReadDto>> GetAllGuidelinetypes () {
            var allGuidelinetypes = _repository.GetAll<Guidelinetypes> ();
            return Ok (_mapper.Map<IEnumerable<GuidelineTypeReadDto>> (allGuidelinetypes));
        }

        [HttpGet ("{id}", Name = "GetFirstGuidelineType")]
        public ActionResult<GuidelineTypeReadDto> GetFirstGuidelineType (int id) {
            var theGuidelineType = _repository.GetFirst<Guidelinetypes> (x => x.Id == id);
            if (theGuidelineType != null) {
                return Ok (_mapper.Map<GuidelineTypeReadDto> (theGuidelineType));
            } else {
                return NotFound ();
            }

        }

        [HttpPost]
        public ActionResult<GuidelineTypeReadDto> CreateGuidelineType (GuidelineTypeCreateDto guidelineTypeCreateDto) {
            var guidelineTypeModel = _mapper.Map<Guidelinetypes> (guidelineTypeCreateDto);
            _repository.Create<Guidelinetypes> (guidelineTypeModel);
            _repository.SaveChanges ();

            var guidelineTypeReadDto = _mapper.Map<GuidelineTypeReadDto> (guidelineTypeModel);

            return CreatedAtRoute (nameof (GetFirstGuidelineType), new { id = guidelineTypeReadDto.Id }, guidelineTypeReadDto);
        }

        [HttpPut ("{id}")]
        public ActionResult<GuidelineTypeUpdateDto> UpdateGuidelineType (GuidelineTypeUpdateDto guidelineTypeUpdateDto, int id) {
            var guidelineTypeFromRepo = _repository.GetFirst<Guidelinetypes> (x => x.Id == id);
            if (guidelineTypeFromRepo == null) {
                return NotFound ();
            }
            _mapper.Map (guidelineTypeUpdateDto, guidelineTypeFromRepo);

            _repository.Update<Guidelinetypes> (guidelineTypeFromRepo);
            _repository.SaveChanges ();

            return NoContent ();
        }

        [HttpPatch ("{id}")]
        public ActionResult PartialUpdateGuidelineType (int id, JsonPatchDocument<GuidelineTypeUpdateDto> jsonPatchDocument) {
            var guidelineTypeFromRepo = _repository.GetFirst<Guidelinetypes> (x => x.Id == id);
            if (guidelineTypeFromRepo == null) {
                return NotFound ();
            }

            var guidelineTypeToPatch = _mapper.Map<GuidelineTypeUpdateDto> (guidelineTypeFromRepo);
            jsonPatchDocument.ApplyTo (guidelineTypeToPatch, ModelState);
            if (!TryValidateModel (guidelineTypeToPatch)) {
                return ValidationProblem (ModelState);
            }

            _mapper.Map (guidelineTypeToPatch, guidelineTypeFromRepo);
            _repository.Update<Guidelinetypes> (guidelineTypeFromRepo);
            _repository.SaveChanges ();

            return NoContent ();

        }

        [HttpDelete ("{id}")]
        public ActionResult DeleteGuidelineType (int id) {
            var guidelineTypeFromRepo = _repository.GetFirst<Guidelinetypes> (x => x.Id == id);
            if (guidelineTypeFromRepo == null) {
                return NotFound ();
            }

            _repository.Delete<Guidelinetypes> (guidelineTypeFromRepo);
            _repository.SaveChanges ();

            return NoContent ();
        }
    }
}