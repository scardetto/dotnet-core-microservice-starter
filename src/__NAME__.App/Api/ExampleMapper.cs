using AutoMapper;
using __NAME__.App.Domain;
using __NAME__.Messages.Examples;

namespace __NAME__.App.Api
{
    public class ExampleMapper : Profile
    {
        public ExampleMapper()
        {
            CreateMap<ExampleEntity, ExampleModel>()
                .ForMember(x => x.Status, m => m.MapFrom(x => (int)x.Status))
                ;
        }
    }
}