using Container.Base;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public IActionResult CreateActionResultInstance<T>(ResponseModel<T> response) where T : class
        {
            return new ObjectResult(response) { StatusCode = response.StatusCode };
        }
    }
}
