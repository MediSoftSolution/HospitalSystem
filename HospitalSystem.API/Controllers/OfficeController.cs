using HospitalSystem.Application.Features.Offices.Commands.CreateOffice;
using HospitalSystem.Application.Features.Offices.Commands.DeleteOffice;
using HospitalSystem.Application.Features.Offices.Commands.UpdateDoctor;
using HospitalSystem.Application.Features.Offices.Queries.GetAllOffices;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OfficeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OfficeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOffices()
        {
            var response = await _mediator.Send(new GetAllOfficesQueryRequest());
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOffice(CreateOfficeCommandRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOffice(UpdateOfficeCommandRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOffice(DeleteOfficeCommandRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }
    }

}
