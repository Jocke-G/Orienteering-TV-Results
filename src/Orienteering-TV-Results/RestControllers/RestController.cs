using Microsoft.AspNetCore.Mvc;

namespace Orienteering_TV_Results.RestControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "API Response :)";
        }
    }
}
