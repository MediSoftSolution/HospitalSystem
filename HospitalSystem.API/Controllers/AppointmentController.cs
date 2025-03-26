using HospitalSystem.Application.Features.Appointment.Commands.CreateAppointment;
using HospitalSystem.Application.Features.Appointment.Commands.DeleteAppointment;
using HospitalSystem.Application.Features.Appointment.Commands.UpdateAppointment;
using HospitalSystem.Application.Features.Appointment.Queries.GetAppointmentById;
using HospitalSystem.Application.Features.Appointment.Queries.GetAppointmentsByPatient;
using HospitalSystem.Application.Features.Doctors.Queries.GetAvailableTimes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HospitalSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AppointmentController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("availabletimes")]
        [ProducesResponseType(typeof(List<DateTime>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAvailableTimes([FromQuery] int? doctorId, [FromQuery] DateTime? date)
        {
            if (!doctorId.HasValue || !date.HasValue)
                return BadRequest("DoctorId and date must be provided.");

            var availableTimes = await _mediator.Send(new GetAvailableTimesQueryRequest(doctorId.Value, date.Value));

            if (availableTimes is null || availableTimes.Count == 0)
                return NotFound("No available times for the selected date.");

            return Ok(availableTimes);
        }

        [HttpPost("createappointment")]
        [ProducesResponseType(typeof(CreateAppointmentCommandResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentCommandRequest request)
        {
            if (request is null)
                return BadRequest("Invalid appointment request.");

            var result = await _mediator.Send(request);

            if (!result.Success)
                return BadRequest(result.Message);

            return CreatedAtAction(nameof(GetAppointmentById), new { id = result.AppointmentId }, result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetAppointmentByIdQueryResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAppointmentById(int id)
        {
            var appointment = await _mediator.Send(new GetAppointmentByIdQueryRequest(id));

            if (appointment is null)
                return NotFound("Appointment not found.");

            return Ok(appointment);
        }

        [HttpGet("patient/{patientId}")]
        [ProducesResponseType(typeof(List<GetAppointmentsByPatientQueryResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAppointmentsByPatient(Guid patientId)
        {
            var appointments = await _mediator.Send(new GetAppointmentsByPatientQueryRequest(patientId));

            if (appointments is null || appointments.Count == 0)
                return NotFound("No appointments found for this patient.");

            return Ok(appointments);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            try
            {
                await _mediator.Send(new DeleteAppointmentCommandRequest(id));
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update")]
        [ProducesResponseType(typeof(UpdateAppointmentCommandResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateAppointment([FromBody] UpdateAppointmentCommandRequest request)
        {
            if (request is null)
                return BadRequest("Invalid update request.");

            var result = await _mediator.Send(request);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }
    }
}
