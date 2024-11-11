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
            var response = await _mediator.Send(new GetAllDoctorsQueryRequest());

            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateDoctorCommandRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateDoctorCommandRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> DeleteProduct(DeleteDoctorCommandRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }
    }
}
