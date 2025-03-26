using AutoMapper;
using HospitalSystem.Application.Bases;
using HospitalSystem.Application.Interfaces.UnitOfWorks;
using HospitalSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Application.Features.Tests.Commands.UpdateTest
{
    public class UpdateTestCommandHandler : BaseHandler, IRequestHandler<UpdateTestCommandRequest, UpdateTestCommandResponse>
    {
        public UpdateTestCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<UpdateTestCommandResponse> Handle(UpdateTestCommandRequest request, CancellationToken cancellationToken)
        {
            var test = await unitOfWork.GetReadRepository<Test>().GetAsync(t => t.Id == request.Id);

            if (test == null)
                return new UpdateTestCommandResponse(false, "Test not found.");

            test.RefDoctor = request.RefDoctor;
            test.IsReady = request.IsReady;
            test.UserName = request.UserName;

            if (request.TestResult != null)
            {
                test.TestResult ??= new TestResult();

                test.TestResult.TestNameAndResultEntry = request.TestResult.TestNameAndResultEntries?
                    .Select(entry => new TestNameAndResultEntry { Value = entry.Value })
                    .ToList();

                test.TestResult.TestImageUrl = request.TestResult.TestImageUrls ?? new List<string>();
                test.TestResult.TestConclusion = request.TestResult.TestConclusion;
            }

            await unitOfWork.GetWriteRepository<Test>().UpdateAsync(test);
            await unitOfWork.SaveAsync();

            return new UpdateTestCommandResponse(true, "Test updated successfully.");
        }
    }
}
