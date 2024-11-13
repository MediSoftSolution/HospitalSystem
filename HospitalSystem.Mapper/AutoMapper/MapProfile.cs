using AutoMapper;
using HospitalSystem.Domain.Entities;
using HospitalSystem.Application.DTOs;
using HospitalSystem.Application.Features.Offices.Commands.CreateOffice;
using HospitalSystem.Application.Features.Offices.Queries.GetAllOffices;

namespace HospitalSystem.Mapper.AutoMapper
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<CreateOfficeCommandRequest, Office>()
               .ReverseMap();

            CreateMap<WorkingTimeDto, WorkingTime>()
                 .ForMember(dest => dest.Start, opt => opt.MapFrom(src => TimeSpan.Parse(src.Start)))
                 .ForMember(dest => dest.End, opt => opt.MapFrom(src => TimeSpan.Parse(src.End)))
                 .ReverseMap();

            CreateMap<PhotoDto, Photo>().ReverseMap();
            CreateMap<GetAllOfficesQueryResponse, Office>().ReverseMap();
            CreateMap<GetAllOfficesQueryRequest, Office>().ReverseMap();
        }
    }
}
