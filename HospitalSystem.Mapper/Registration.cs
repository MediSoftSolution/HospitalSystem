using Microsoft.Extensions.DependencyInjection;
using AutoMapper;

namespace HospitalSystem.Mapper
{
    public static class Registration
    {
        public static void AddCustomMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MapProfile));
        }
    }
}
