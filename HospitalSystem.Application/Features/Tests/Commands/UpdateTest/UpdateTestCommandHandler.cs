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

            if (test == null) return new UpdateTestCommandResponse(false, "Test not found.");

            test.RefDoctor = request.RefDoctor;
            test.IsReady = request.IsReady;
            test.UserName = request.UserName;
            test.TestConclusion = request.TestConclusion;

            if (request.TestNameAndResultEntries != null && request.TestNameAndResultEntries.Any())
            {
                foreach (var entryRequest in request.TestNameAndResultEntries)
                {
                    var existingEntry = test.TestNameAndResultEntry.FirstOrDefault(e => e.Key == entryRequest.Key);

                    if (existingEntry != null)
                    {
                        existingEntry.Value = entryRequest.Value;
                    }
                }
            }

            if (request.TestImageUrls != null)
            {
                test.TestImages ??= new List<TestImage>();
                foreach (var item in request.TestImageUrls)
                {
                    TestImage testImage = new() { ImageUrl = item, TestId = test.Id};
                    test.TestImages.Add(testImage);
                }
                
            }

            await unitOfWork.GetWriteRepository<Test>().UpdateAsync(test);
            await unitOfWork.SaveAsync();

            return new UpdateTestCommandResponse(true, "Test updated successfully.");
        }
    }
}
