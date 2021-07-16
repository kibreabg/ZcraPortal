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

    [Route("api/memos")]
    [ApiController]
    public class MemosController : ControllerBase
    {
        private readonly IZcraPortalRepo _repository;
        private IMapper _mapper;
        private IConfiguration _configuration;

        public MemosController(IZcraPortalRepo repository, IMapper mapper, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult<IEnumerable<MemoReadDto>> GetAllMemos()
        {
            var allMemos = _repository.GetAll<Memos>();
            return Ok(_mapper.Map<IEnumerable<MemoReadDto>>(allMemos));
        }

        [HttpGet("{id}", Name = "GetFirstMemo")]
        public ActionResult<MemoReadDto> GetFirstMemo(int id)
        {
            var theMemo = _repository.GetFirst<Memos>(x => x.Id == id);
            if (theMemo != null)
            {
                return Ok(_mapper.Map<MemoReadDto>(theMemo));
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPost]
        public ActionResult<MemoReadDto> CreateMemo(MemoCreateDto memoCreateDto)
        {
            var memoModel = _mapper.Map<Memos>(memoCreateDto);
            _repository.Create<Memos>(memoModel);
            _repository.SaveChanges();

            var memoReadDto = _mapper.Map<MemoReadDto>(memoModel);

            return CreatedAtRoute(nameof(GetFirstMemo), new { id = memoReadDto.Id }, memoReadDto);
        }

        [Route("upload")]
        [HttpPost]
        public async Task<IActionResult> OnPostUploadAsync([FromForm] UploadFile uploadFile)
        {
            try
            {
                var uploadFolder = "Memos";
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
        public ActionResult<MemoUpdateDto> UpdateMemo(MemoUpdateDto memoUpdateDto, int id)
        {
            var memoFromRepo = _repository.GetFirst<Memos>(x => x.Id == id);
            if (memoFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(memoUpdateDto, memoFromRepo);

            _repository.Update<Memos>(memoFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult PartialUpdateMemo(int id, JsonPatchDocument<MemoUpdateDto> jsonPatchDocument)
        {
            var memoFromRepo = _repository.GetFirst<Memos>(x => x.Id == id);
            if (memoFromRepo == null)
            {
                return NotFound();
            }

            var memoToPatch = _mapper.Map<MemoUpdateDto>(memoFromRepo);
            jsonPatchDocument.ApplyTo(memoToPatch, ModelState);
            if (!TryValidateModel(memoToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(memoToPatch, memoFromRepo);
            _repository.Update<Memos>(memoFromRepo);
            _repository.SaveChanges();

            return NoContent();

        }

        [HttpDelete("{id}")]
        public ActionResult DeleteMemo(int id)
        {
            var memoFromRepo = _repository.GetFirst<Memos>(x => x.Id == id);
            if (memoFromRepo == null)
            {
                return NotFound();
            }

            _repository.Delete<Memos>(memoFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}