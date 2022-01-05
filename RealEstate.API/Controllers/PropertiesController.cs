using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using RealEstate.Features.DTOs.Properties;
using RealEstate.Features.Properties.Requests.Queries;
using RealEstate.Features.DTOs.Common;
using RealEstate.Features.Properties.Requests.Commands;
using System;

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

        [HttpPost("get")]
        public async Task<IActionResult> GetPropertiesAsync(PageListDTO model)
        {
            return Ok(await _mediator.Send(new GetPropertiesListRequest { page = model.Page, pageSize = model.PageSize }));
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePropertyAsync(UpdatePropertyDTO model)
        {
            return Ok(await _mediator.Send(new UpdatePropertyCommand() { PropertyToUpdate = model }));
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePropertyAsync(CreatePropertyDTO model)
        {
            return Ok(await _mediator.Send(new CreatePropertyCommand() { PropertyViewModel = model }));
        }

        [HttpDelete("delete/{Id}")]
        public async Task<IActionResult> DeletePropertyAsync(Guid Id)
        {
            return Ok(await _mediator.Send(new DeletePropertyCommand() { PropertyId = Id }));
        }
    }
}
