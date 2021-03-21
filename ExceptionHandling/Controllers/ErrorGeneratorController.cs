using Microsoft.AspNetCore.Mvc;

namespace Exceptionhandling.ExceptionHandling.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ErrorGeneratorController : ControllerBase
    {
        [HttpGet("NotFound")]
        public ActionResult GetNotFound()
        {
            return NotFound();
        }

        [HttpGet("ServerError")]
        public ActionResult GetServerError()
        {
            dynamic upper = 10;
            dynamic lower = 0;

            return upper / lower;
        }

        [HttpGet("BadRequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest("This was not a good request");
        }
    }
}