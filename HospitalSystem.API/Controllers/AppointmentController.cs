using HospitalSystem.Application.Features.Appointment.Commands.CreateAppointment;
using HospitalSystem.Application.Features.Doctors.Queries.GetAvailableTimes;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace HospitalSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AppointmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("available-times")]
        public async Task<IActionResult> GetAvailableTimes([FromQuery] int doctorId, [FromQuery] DateTime date)
        {
            var availableTimes = await _mediator.Send(new GetAvailableTimesQueryRequest(doctorId, date));

            if (availableTimes == null || availableTimes.Count == 0)
            {
                return NotFound("No available times for the selected date.");
            }

            return Ok(availableTimes);
        }

        //[HttpPost("create")]
        //public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentCommandRequest request)
        //{
        //    var result = await _mediator.Send(request);

        //    if (!result.Success)
        //    {
        //        return BadRequest(result.Message);
        //    }

        //    return CreatedAtAction(nameof(GetAppointmentById), new { id = result.AppointmentId }, result);
        //}

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetAppointmentById(Guid id)
        //{
        //    var appointment = await _mediator.Send(new GetAppointmentByIdQueryRequest { Id = id });

        //    if (appointment == null)
        //    {
        //        return NotFound("Appointment not found.");
        //    }

        //    return Ok(appointment);
        //}

        //[HttpGet("patient/{patientId}")]
        //public async Task<IActionResult> GetAppointmentsByPatient(Guid patientId)
        //{
        //    var appointments = await _mediator.Send(new GetAppointmentsByPatientQueryRequest { PatientId = patientId });

        //    if (appointments == null || appointments.Count == 0)
        //    {
        //        return NotFound("No appointments found for this patient.");
        //    }

        //    return Ok(appointments);
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> CancelAppointment(Guid id)
        //{
        //    var result = await _mediator.Send(new CancelAppointmentCommand { AppointmentId = id });

        //    if (!result.Success)
        //    {
        //        return BadRequest(result.Message);
        //    }

        //    return NoContent();
        //}
    }
}
