using System.Reflection;
using AutoMapper;
using StructureMap;

namespace __NAME__.App.Infrastructure.Registries
{
    public class AutoMapperRegistry : Registry
    {
        public AutoMapperRegistry()
        {
            ForSingletonOf<IMapper>().Use(c => CreateMapper(c));
        }

        public static IMapper CreateMapper(IContext context)
        {
            var config = new MapperConfiguration(c => {
                c.AddProfiles(Assembly.GetExecutingAssembly());
            });

            return new Mapper(config);
        }
    }
}