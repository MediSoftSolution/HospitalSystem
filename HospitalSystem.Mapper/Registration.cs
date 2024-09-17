using HospitalSystem.Mapper.AutoMapper;
using HospitalSystem.Application.Interfaces.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace HospitalSystem.Mapper
{
    public static class Registration
    {
        public static void AddCustomMapper(this IServiceCollection services)
        {
            services.AddSingleton<IMapper, AutoMapper.Mapper>();
        }
    }
}
