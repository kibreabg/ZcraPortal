using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ZcraPortal.Data;
//using ZcraPortal.Model;

namespace ZcraPortal.Controllers {

    [Route ("api/guidelines")]
    [ApiController]
    public class GuidelinesController : ControllerBase {
        private readonly IZcraPortalRepo _repository;

        public GuidelinesController (IZcraPortalRepo repository) {
            _repository = repository;
        }

        /*[HttpGet]
        public ActionResult<IEnumerable<Guidelines>> GetAll () {
            var allValues = _repository.GetAll<Guidelines> ();
            return Ok (allValues);
        }*/
    }
}