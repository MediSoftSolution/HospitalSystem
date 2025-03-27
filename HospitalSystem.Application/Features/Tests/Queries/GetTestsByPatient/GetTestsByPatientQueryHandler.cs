using HospitalSystem.Application.Features.Tests.Queries.GetTestById;
using HospitalSystem.Application.Interfaces.UnitOfWorks;
using HospitalSystem.Domain.Entities;
using MediatR;

namespace HospitalSystem.Application.Features.Tests.Queries.GetTestsByPatient
{
    public class GetTestsByPatientQueryHandler : IRequestHandler<GetTestsByPatientQueryRequest, List<GetTestsByPatientQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTestsByPatientQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<GetTestsByPatientQueryResponse>> Handle(GetTestsByPatientQueryRequest request, CancellationToken cancellationToken)
        {
            var tests = await _unitOfWork.GetReadRepository<Test>().GetAllAsync(t => t.UserName == request.PatientName);

            if (tests == null || !tests.Any())
                return new List<GetTestsByPatientQueryResponse>();

            return tests.Select(test => new GetTestsByPatientQueryResponse(
                test.Id,
                test.TestName,
                test.RefDoctor,
                test.IsReady,
                test.UserName   
            )).ToList();
        }
    }
}
