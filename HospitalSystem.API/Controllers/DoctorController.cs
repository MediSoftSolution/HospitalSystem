using HospitalSystem.Application.Features.Doctors.Commands.CreateDoctor;
using HospitalSystem.Application.Features.Doctors.Commands.DeleteDoctor;
using HospitalSystem.Application.Features.Doctors.Commands.UpdateDoctor;
using HospitalSystem.Application.Features.Doctors.Queries.GetAllDoctors;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HospitalSystem.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DoctorController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllDoctors()
        {
            var doctors = await _mediator.Send(new GetAllDoctorsQueryRequest());

            if (doctors == null || doctors.Count == 0)
            {
                return NotFound("No doctors found.");
            }

            return Ok(doctors);
        }
        [HttpPost]
        public async Task<IActionResult> CreateDoctor(CreateDoctorCommandRequest request)
        {
            var result = await _mediator.Send(request);

            if (result.Equals(Unit.Value))
            {
                return Ok();
            }

            return BadRequest("Failed to create doctor.");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateDoctor(UpdateDoctorCommandRequest request)
        {
            var result = await _mediator.Send(request); 

            if (result.Equals(Unit.Value)) 
            {
                return Ok();
            }

            return NotFound("Doctor not found.");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteDoctor(DeleteDoctorCommandRequest request)
        {
            var result = await _mediator.Send(request);

            if (result.Equals(Unit.Value))
            {
                return NoContent(); 
            }

            return NotFound("Doctor not found.");
        }
    }
}
