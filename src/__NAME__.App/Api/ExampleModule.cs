using System.Data;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dapper;
using Nancy;
using Rebus.Bus;
using __NAME__.App.Domain;
using __NAME__.App.Infrastructure.Bootstrapping.Nancy;
using __NAME__.Messages.Examples;

namespace __NAME__.App.Api
{
    public class ExampleModule : NancyModule
    {
        public ExampleModule(IDbConnection connection, IMapper mapper, IBus bus)
        {
            Get("/examples", _ => connection.GetList<ExampleEntity>()
                .AsQueryable()
                .ProjectTo<ExampleModel>(mapper.ConfigurationProvider)
                .ToList());

            Get("/example/{id:int}", _ => {
                var entity = connection.Get<ExampleEntity>((int)_.id);
                return mapper.Map<ExampleEntity, ExampleModel>(entity);
            });

            Post("/examples", _ => {
                var model = this.BindAndValidateModel<NewExampleModel>();

                var entity = new ExampleEntity(model.Name);
                entity.Id = connection.Insert(entity).GetValueOrDefault(0);

                return new NewExampleCreatedModel { Id = entity.Id };
            });

            Post("/examples/close", async _ => {
                var model = this.BindAndValidateModel<CloseExampleModel>();
                await bus.Send(new CloseExampleCommand {Id = model.Id});
                return HttpStatusCode.OK;
            });

            Delete("/example/{id:int}", _ => {
                connection.Delete<ExampleEntity>((int)_.id);
                return HttpStatusCode.OK;
            });
        }
    }
}