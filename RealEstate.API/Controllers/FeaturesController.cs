using MediatR;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Features.DTOs.PropertyFeatures;
using RealEstate.Features.PropertyFeatures.Requests.Commands;
using RealEstate.Features.PropertyFeatures.Requests.Queries;
using System;
using System.Threading.Tasks;

namespace RealEstate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FeaturesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _mediator.Send(new GetFeaturesRequest()));
        }

        [HttpGet("getDetails/{Id}")]
        public async Task<IActionResult> GetDetailsAsync(Guid Id)
        {
            return Ok(await _mediator.Send(new GetFeatureDetailsRequest() { Id = Id }));
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync(CreatePropertyFeatureDTO model)
        {
            return Ok(await _mediator.Send(new CreateFeatureCommand() { CreatePropertyFeatureDTO = model }));
        }
    }
}
