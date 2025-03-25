using AutoMapper;
using HospitalSystem.Application.DTOs;
using HospitalSystem.Application.Features.Doctors.Commands.CreateDoctor;
using HospitalSystem.Application.Features.Doctors.Commands.UpdateDoctor;
using HospitalSystem.Application.Features.Doctors.Queries.GetAllDoctors;
using HospitalSystem.Application.Features.Offices.Commands.CreateOffice;
using HospitalSystem.Application.Features.Offices.Queries.GetAllOffices;
using HospitalSystem.Application.Features.Specialties.Queries.GetAllSpecialities;
using HospitalSystem.Domain.Entities;

public class MapProfile : Profile
{
    public MapProfile()
    {
        CreateMap<CreateOfficeCommandRequest, Office>().ReverseMap();
        CreateMap<GetAllOfficesQueryResponse, Office>().ReverseMap();
        CreateMap<GetAllOfficesQueryRequest, Office>().ReverseMap();

        CreateMap<WorkingTimeDto, WorkingTime>()
            .ForMember(dest => dest.Start, opt => opt.MapFrom(src => TimeSpan.Parse(src.Start)))
            .ForMember(dest => dest.End, opt => opt.MapFrom(src => TimeSpan.Parse(src.End)))
            .ReverseMap()
            .ForMember(dest => dest.Start, opt =>
                opt.MapFrom(src => src.Start.ToString(@"hh\:mm\:ss")))
            .ForMember(dest => dest.End, opt =>
                opt.MapFrom(src => src.End.ToString(@"hh\:mm\:ss")));

        CreateMap<PhotoDto, Photo>().ReverseMap();

        CreateMap<CreateDoctorCommandRequest, Doctor>()
            .ForMember(dest => dest.WorkingTimes, opt => opt.MapFrom(src => src.WorkingTimes.Select(wt => new WorkingTime
            {
                Day = wt.Key,
                Start = wt.Value.Start,
                End = wt.Value.End
            })))
            .ForMember(dest => dest.Appointments, opt => opt.Ignore())
            .ForMember(dest => dest.WorkingOffice, opt => opt.Ignore())
            .ForMember(dest => dest.Photo, opt => opt.Ignore())
            .ForMember(dest => dest.Specialty, opt => opt.Ignore());

            CreateMap<Doctor, GetAllDoctorsQueryResponse>()
            .ForMember(dest => dest.DoctorId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User.Fullname))
            .ForMember(dest => dest.WorkingTimes, opt => opt.MapFrom(src =>
                src.WorkingTimes != null
                    ? src.WorkingTimes
                        .GroupBy(wt => wt.Day) // Ensure no duplicate keys
                        .ToDictionary(
                            wt => wt.Key,
                            wt => new WorkingTimeDto(
                                wt.Key,
                                wt.First().Start.ToString(@"hh\:mm\:ss"), // Ensure First() is safe
                                wt.First().End.ToString(@"hh\:mm\:ss")   // Ensure First() is safe
                            )
                        )
                    : new Dictionary<DayOfWeek, WorkingTimeDto>() // Default to empty dictionary
            ));


        // Explicitly define mapping for the list of doctors to the response.
        CreateMap<List<Doctor>, List<GetAllDoctorsQueryResponse>>()
            .ConvertUsing((src, dest, context) =>
                src.Select(doc => context.Mapper.Map<GetAllDoctorsQueryResponse>(doc)).ToList());

        CreateMap<GetAllDoctorsQueryResponse, Doctor>()
            .ForMember(dest => dest.WorkingTimes, opt => opt.MapFrom(src =>
                src.WorkingTimes.ToDictionary(wt => wt.Key, wt => new WorkingTime
                {
                    Day = wt.Key,
                    Start = TimeSpan.Parse(wt.Value.Start),
                    End = TimeSpan.Parse(wt.Value.End)
                })
            ));

        CreateMap<UpdateDoctorCommandRequest, Doctor>()
            .ForMember(dest => dest.WorkingTimes, opt => opt.MapFrom(src =>
                src.WorkingTimes.Select(wt => new WorkingTime
                {
                    Day = wt.Key,
                    Start = TimeSpan.Parse(wt.Value.Start),
                    End = TimeSpan.Parse(wt.Value.End)
                }).ToList()));

        CreateMap<Specialty, GetAllSpecialitiesQueryRequest>().ReverseMap();
    }
}
