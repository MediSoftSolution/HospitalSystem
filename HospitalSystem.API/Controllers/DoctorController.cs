using HospitalSystem.Application.Features.Doctors.Commands.CreateDoctor;
using HospitalSystem.Application.Features.Doctors.Commands.DeleteDoctor;
using HospitalSystem.Application.Features.Doctors.Commands.UpdateDoctor;
using HospitalSystem.Application.Features.Doctors.Queries.GetAllDoctors;
using HospitalSystem.Application.Features.Doctors.Queries.GetAlternativeDoctors;
using HospitalSystem.Application.Features.Doctors.Queries.GetAvailableTimes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HospitalSystem.API.Controllers
{
    [Route("api/doctors")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DoctorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<GetAllDoctorsQueryResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
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
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateDoctor([FromBody] CreateDoctorCommandRequest request)
        {
            var result = await _mediator.Send(request);

            if (result == Unit.Value)
            {
                return StatusCode((int)HttpStatusCode.Created);
            }

            return BadRequest("Failed to create doctor.");
        }

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateDoctor(int id, [FromBody] UpdateDoctorCommandRequest request)
        {
            if (id != request.Id)
            {
                return BadRequest("Doctor ID mismatch.");
            }

            var result = await _mediator.Send(request);

            if (result == Unit.Value)
            {
                return Ok("Doctor updated successfully.");
            }

            return NotFound("Doctor not found.");
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var result = await _mediator.Send(new DeleteDoctorCommandRequest { Id = id });

            if (result == Unit.Value)
            {
                return NoContent();
            }

            return NotFound("Doctor not found.");
        }

        [HttpGet("alternative")]
        [ProducesResponseType(typeof(ICollection<GetAlternativeDoctorsQueryResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAlternativeDoctors([FromQuery] string specialityName, [FromQuery] DateTime dateTime)
        {
            var request = new GetAlternativeDoctorsQueryRequest
            {
                SpecialityName = specialityName,
                DateTime = dateTime
            };

            var alternativeDoctors = await _mediator.Send(request);

            if (alternativeDoctors == null || alternativeDoctors.Count == 0)
            {
                return NotFound("No alternative doctors available for the selected specialty and date.");
            }

            return Ok(alternativeDoctors);
        }

        [HttpGet("{doctorId}/available-times")]
        [ProducesResponseType(typeof(ICollection<TimeSpan>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAvailableTimes(int doctorId, [FromQuery] DateTime date)
        {
            var request = new GetAvailableTimesQueryRequest(doctorId, date);

            var availableTimes = await _mediator.Send(request);

            if (availableTimes == null || availableTimes.Count == 0)
            {
                return NotFound("No available times found for the selected doctor on the given date.");
            }

            return Ok(availableTimes);
        }
    }
}
