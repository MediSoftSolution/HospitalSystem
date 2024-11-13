using AutoMapper;
using AutoMapper.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalSystem.Mapper.AutoMapper
{
    public class Mapper : Application.Interfaces.AutoMapper.IMyMapper
    {
        public static List<TypePair> typePairs = new();
        private IMapper MapperContainer;

        public TDestination Map<TDestination, TSource>(TSource source, string? ignore = null)
        {
            RegisterTypePair<TSource, TDestination>();
            ConfigureMappings(ignore);
            return MapperContainer.Map<TSource, TDestination>(source);
        }

        public IList<TDestination> Map<TDestination, TSource>(IList<TSource> source, string? ignore = null)
        {
            RegisterTypePair<TSource, TDestination>();
            ConfigureMappings(ignore); 
            return MapperContainer.Map<IList<TSource>, IList<TDestination>>(source);
        }

        public TDestination Map<TDestination>(object source, string? ignore = null)
        {
            ConfigureMappings(ignore);
            return MapperContainer.Map<TDestination>(source);
        }

        public IList<TDestination> Map<TDestination>(IList<object> source, string? ignore = null)
        {
            ConfigureMappings(ignore);
            return MapperContainer.Map<IList<TDestination>>(source);
        }

        private void ConfigureMappings(string? ignore = null, int depth = 5)
        {
            if (MapperContainer == null) 
            {
                var config = new MapperConfiguration(cfg =>
                {
                    foreach (var typePair in typePairs)
                    {
                        var map = cfg.CreateMap(typePair.SourceType, typePair.DestinationType)
                                     .MaxDepth(depth)
                                     .ReverseMap();

                        if (ignore != null)
                        {
                            map.ForMember(ignore, options => options.Ignore());
                        }
                    }
                });

                MapperContainer = config.CreateMapper();
            }
        }

        public static void RegisterTypePair<TSource, TDestination>()
        {
            var typePair = new TypePair(typeof(TSource), typeof(TDestination));
            if (!typePairs.Any(tp => tp.SourceType == typePair.SourceType && tp.DestinationType == typePair.DestinationType))
            {
                typePairs.Add(typePair);
            }
        }
    }
}
