using HospitalSystem.Application.Features.Specialities.Commands.UpdateSpecialty;
using HospitalSystem.Application.Features.Specialties.Commands.CreateSpeciality;
using HospitalSystem.Application.Features.Specialties.Commands.DeleteSpeciality;
using HospitalSystem.Application.Features.Specialties.Queries.GetAllSpecialities;
using HospitalSystem.Application.Features.Specialties.Queries.GetSpecialityById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HospitalSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialtyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SpecialtyController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<GetAllSpecialitiesQueryResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllSpecialities()
        {
            var response = await _mediator.Send(new GetAllSpecialitiesQueryRequest());
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<GetAllSpecialitiesQueryResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetSpecialityById()
        {
            var response = await _mediator.Send(new GetSpecialityByIdQueryRequest());
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateSpeciality([FromBody] CreateSpecialityCommandRequest request)
        {
            if (request is null)
                return BadRequest("Invalid speciality data.");

            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateSpeciality(int id, [FromBody] UpdateSpecialityCommandRequest request)
        {
            if (request is null || request.Id != id)
                return BadRequest("Invalid update request.");

            var result = await _mediator.Send(request);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteSpeciality(int id)
        {
            var result = await _mediator.Send(new DeleteSpecialityCommandRequest(id));

            if (!result)
                return NotFound();

            return NoContent();

        }
    }
}
