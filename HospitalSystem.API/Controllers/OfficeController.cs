using HospitalSystem.Application.Features.Doctors.Commands.CreateDoctor;
using HospitalSystem.Application.Features.Doctors.Commands.DeleteDoctor;
using HospitalSystem.Application.Features.Offices.Commands.CreateOffice;
using HospitalSystem.Application.Features.Offices.Commands.DeleteOffice;
using HospitalSystem.Application.Features.Offices.Commands.UpdateDoctor;
using HospitalSystem.Application.Features.Offices.Queries.GetAllOffices;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HospitalSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OfficeController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }


        [HttpGet]
        [ProducesResponseType(typeof(List<GetAllOfficesQueryResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllOffices()
        {
            var response = await _mediator.Send(new GetAllOfficesQueryRequest());
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateOffice([FromBody] CreateOfficeCommandRequest request)
        {
            if (request is null)
                return BadRequest("Invalid office data.");

            var result = await _mediator.Send(request);
            return Ok(result);
        }


        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateOffice(Guid id, [FromBody] UpdateOfficeCommandRequest request)
        {
            if (request is null)
                return BadRequest("Invalid update request.");

            var result = await _mediator.Send(request);

            if (!result)
                return NotFound(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteOffice(int id)
        {
            var result = await _mediator.Send(new DeleteOfficeCommandRequest { Id = id });

            if (!result)
                return NotFound(result);

            return NoContent();
        }
    }
}
