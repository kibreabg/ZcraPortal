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

    [Route("api/prescriptiontools")]
    [ApiController]
    public class PrescriptiontoolsController : ControllerBase
    {
        private readonly IZcraPortalRepo _repository;
        private IMapper _mapper;
        private IConfiguration _configuration;

        public PrescriptiontoolsController(IZcraPortalRepo repository, IMapper mapper, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PrescriptiontoolReadDto>> GetAllPrescriptiontools()
        {
            var allPrescriptiontools = _repository.GetAll<Prescriptiontools>();
            return Ok(_mapper.Map<IEnumerable<PrescriptiontoolReadDto>>(allPrescriptiontools));
        }

        [HttpGet("children/{parentId}")]
        public ActionResult<IEnumerable<PrescriptiontoolReadDto>> GetChildrenPrescriptiontools(int parentId)
        {
            var childrenPrescriptiontools = _repository.GetSomeById<Prescriptiontools>(x => x.ParentId == parentId);
            return Ok(_mapper.Map<IEnumerable<PrescriptiontoolReadDto>>(childrenPrescriptiontools));
        }

        [HttpGet("{id}", Name = "GetFirstPrescriptiontool")]
        public ActionResult<PrescriptiontoolReadDto> GetFirstPrescriptiontool(int id)
        {
            var thePrescriptiontool = _repository.GetFirst<Prescriptiontools>(x => x.Id == id);
            if (thePrescriptiontool != null)
            {
                return Ok(_mapper.Map<PrescriptiontoolReadDto>(thePrescriptiontool));
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPost]
        public ActionResult<PrescriptiontoolReadDto> CreatePrescriptiontool(PrescriptiontoolCreateDto prescriptiontoolCreateDto)
        {
            var prescriptiontoolModel = _mapper.Map<Prescriptiontools>(prescriptiontoolCreateDto);
            _repository.Create<Prescriptiontools>(prescriptiontoolModel);
            _repository.SaveChanges();

            var prescriptiontoolReadDto = _mapper.Map<PrescriptiontoolReadDto>(prescriptiontoolModel);

            return CreatedAtRoute(nameof(GetFirstPrescriptiontool), new { id = prescriptiontoolReadDto.Id }, prescriptiontoolReadDto);
        }

        [HttpPut("{id}")]
        public ActionResult<PrescriptiontoolUpdateDto> UpdatePrescriptiontool(PrescriptiontoolUpdateDto prescriptiontoolUpdateDto, int id)
        {
            var prescriptiontoolFromRepo = _repository.GetFirst<Prescriptiontools>(x => x.Id == id);
            if (prescriptiontoolFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(prescriptiontoolUpdateDto, prescriptiontoolFromRepo);

            _repository.Update<Prescriptiontools>(prescriptiontoolFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult PartialUpdatePrescriptiontool(int id, JsonPatchDocument<PrescriptiontoolUpdateDto> jsonPatchDocument)
        {
            var prescriptiontoolFromRepo = _repository.GetFirst<Prescriptiontools>(x => x.Id == id);
            if (prescriptiontoolFromRepo == null)
            {
                return NotFound();
            }

            var prescriptiontoolToPatch = _mapper.Map<PrescriptiontoolUpdateDto>(prescriptiontoolFromRepo);
            jsonPatchDocument.ApplyTo(prescriptiontoolToPatch, ModelState);
            if (!TryValidateModel(prescriptiontoolToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(prescriptiontoolToPatch, prescriptiontoolFromRepo);
            _repository.Update<Prescriptiontools>(prescriptiontoolFromRepo);
            _repository.SaveChanges();

            return NoContent();

        }

        [HttpDelete("{id}")]
        public ActionResult DeletePrescriptiontool(int id)
        {
            var prescriptiontoolFromRepo = _repository.GetFirst<Prescriptiontools>(x => x.Id == id);
            if (prescriptiontoolFromRepo == null)
            {
                return NotFound();
            }

            _repository.Delete<Prescriptiontools>(prescriptiontoolFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}