using System;
using Nancy;
using Nancy.Responses;

namespace __NAME__.App.Infrastructure.Nancy
{
    public class HttpBadRequestPipeline
    {
        public static readonly Func<NancyContext, Exception, object> OnHttpBadRequest = (ctx, ex) =>
        {
            var requestException = ex as BadRequestException;
            if (requestException == null) return null as Response;

            var data = new {
                errors = requestException.ValidationErrors
            };

            return new JsonResponse(data, new DefaultJsonSerializer(ctx.Environment), ctx.Environment) {
                StatusCode = HttpStatusCode.BadRequest
            };
        };
    }
}
