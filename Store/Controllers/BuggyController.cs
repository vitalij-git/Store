using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly StoreDbContext _Context;

        public BuggyController(StoreDbContext context)
        {
            _Context = context;
        }

        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest() 
        {
            return NotFound(new ApiResponse(404));
        }

        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            
            return Ok();
        }

        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("badrequest/{id}")]
        public ActionResult GetBadRequestByValidation(int id)
        {
            return BadRequest();
        }

    }
}
