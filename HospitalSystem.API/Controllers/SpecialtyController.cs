using HospitalSystem.Application.Features.Specialities.Commands.UpdateSpecialty;
using HospitalSystem.Application.Features.Specialties.Commands.CreateSpeciality;
using HospitalSystem.Application.Features.Specialties.Commands.DeleteSpeciality;
using HospitalSystem.Application.Features.Specialties.Queries.GetAllSpecialities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HospitalSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialtyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SpecialtyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSpecialities()
        {
            var response = await _mediator.Send(new GetAllSpecialitiesQueryRequest());
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSpeciality(CreateSpecialityCommandRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSpeciality(UpdateSpecialityCommandRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSpeciality(DeleteSpecialityCommandRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }
    }
}
