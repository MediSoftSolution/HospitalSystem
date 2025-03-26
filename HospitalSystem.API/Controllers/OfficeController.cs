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

        //[HttpPost]
        //[ProducesResponseType(typeof(CreateOfficeCommandResponse), (int)HttpStatusCode.Created)]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
        //public async Task<IActionResult> CreateOffice([FromBody] CreateOfficeCommandRequest request)
        //{
        //    if (request is null)
        //        return BadRequest("Invalid office data.");

        //    var result = await _mediator.Send(request);
        //    return CreatedAtAction(nameof(GetAllOffices), new { id = result.Id }, result);
        //}

        //[HttpPut("{id}")]
        //[ProducesResponseType(typeof(UpdateOfficeCommandResponse), (int)HttpStatusCode.OK)]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
        //[ProducesResponseType((int)HttpStatusCode.NotFound)]
        //public async Task<IActionResult> UpdateOffice(Guid id, [FromBody] UpdateOfficeCommandRequest request)
        //{
        //    if (request is null || request.Id != id)
        //        return BadRequest("Invalid update request.");

        //    var result = await _mediator.Send(request);

        //    if (!result.Success)
        //        return NotFound(result.Message);

        //    return Ok(result);
        //}

        //[HttpDelete("{id}")]
        //[ProducesResponseType((int)HttpStatusCode.NoContent)]
        //[ProducesResponseType((int)HttpStatusCode.NotFound)]
        //public async Task<IActionResult> DeleteOffice(Guid id)
        //{
        //    var result = await _mediator.Send(new DeleteOfficeCommandRequest(id));

        //    if (!result.Success)
        //        return NotFound(result.Message);

        //    return NoContent();
        //}
    }
}
