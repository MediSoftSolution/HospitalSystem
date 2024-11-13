using HospitalSystem.Mapper.AutoMapper;
using HospitalSystem.Application.Interfaces.AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;

namespace HospitalSystem.Mapper
{
    public static class Registration
    {
        public static void AddCustomMapper(this IServiceCollection services)
        {
            services.AddSingleton<IMyMapper, AutoMapper.Mapper>();
            services.AddAutoMapper(typeof(MapProfile));
        }
    }
}
