using System.Collections.Generic;
using LayoutRestService.Contracts;
using LayoutRestService.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LayoutRestService.Controllers
{
    [ApiController]
    [EnableCors("defaultCorsPolicy")]
    public class LayoutController : ControllerBase
    {
        private readonly ILogger<LayoutController> _logger;
        private readonly ILayoutService _service;

        public LayoutController(ILogger<LayoutController> logger, ILayoutService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("layouts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Layout>> Get()
        {
            return Ok(_service.GetLayouts());
        }

        [HttpGet("layouts/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Layout> GetByName(string name)
        {
            var layout = _service.GetLayoutByName(name);
            if(layout == null)
                return NotFound();

            return Ok(layout);
        }

        [HttpPut("layouts/{name}")]
        public ActionResult<Layout> PutByName(string name, Layout layout)
        {
            if (name != layout.Name)
                return BadRequest();

            return Ok(_service.SaveOrUpdate(layout));
        }
    }
}
