using HospitalSystem.Application.Features.Tests.Commands.CreateTest;
using HospitalSystem.Application.Features.Tests.Commands.CreateTestTemplate;
using HospitalSystem.Application.Features.Tests.Commands.DeleteTest;
using HospitalSystem.Application.Features.Tests.Commands.UpdateTest;
using HospitalSystem.Application.Features.Tests.Commands.UpdateTestTemplate;
using HospitalSystem.Application.Features.Tests.Queries.GetTestById;
using HospitalSystem.Application.Features.Tests.Queries.GetTestsByPatient;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HospitalSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TestController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("patient/{patientId}")]
        [ProducesResponseType(typeof(List<GetTestsByPatientQueryResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetTestsByPatient(string patientName)
        {
            var tests = await _mediator.Send(new GetTestsByPatientQueryRequest(patientName));

            if (tests == null || tests.Count == 0)
                return NotFound("No tests found for this patient.");

            return Ok(tests);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetTestByIdQueryResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetTestById(int id)
        {
            var test = await _mediator.Send(new GetTestByIdQueryRequest(id));

            if (test == null)
                return NotFound("Test not found.");

            return Ok(test);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateTestCommandResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateTest([FromBody] CreateTestCommandRequest request)
        {
            if (request == null)
                return BadRequest("Invalid test data.");

            var result = await _mediator.Send(request);

            return Ok(HttpStatusCode.Created);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UpdateTestCommandResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateTest(int id, [FromBody] UpdateTestCommandRequest request)
        {
            if (request == null || request.Id != id)
                return BadRequest("Invalid update request.");

            var result = await _mediator.Send(request);

            if (!result.Success)
                return NotFound(result.Message);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteTest(int id)
        {
            var result = await _mediator.Send(new DeleteTestCommandRequest(id));

            return NoContent();
        }

        [HttpPost("template")]
        [ProducesResponseType(typeof(CreateTestTemplateCommandResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateTestTemplate([FromBody] CreateTestTemplateCommandRequest request)
        {
            if (request == null)
                return BadRequest("Invalid test data.");

            var result = await _mediator.Send(request);

            return Ok(HttpStatusCode.Created);
        }

        [HttpPut("template/{id}")]
        [ProducesResponseType(typeof(UpdateTestTemplateCommandResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateTestTemplate(int id, [FromBody] UpdateTestTemplateCommandRequest request)
        {
            if (request == null || request.TestId != id)
                return BadRequest("Invalid update request.");

            var result = await _mediator.Send(request);

            if (!result.Success)
                return NotFound(result.Message);

            return Ok(result);
        }
    }
}
