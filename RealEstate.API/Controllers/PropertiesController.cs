using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using RealEstate.Features.DTOs.Properties;
using RealEstate.Features.Properties.Requests.Queries;
using RealEstate.Features.DTOs.Common;

namespace RealEstate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PropertiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> GetPropertiesAsync(PageListDTO model)
        {
            return Ok(await _mediator.Send(new GetPropertiesListRequest { page = model.Page, pageSize = model.PageSize }));
        }
    }
}
