using System.Linq;
using System.Threading.Tasks;
using Arcaim.Assertor.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Arcaim.Assertor
{
    public class AssertorMiddleware : IMiddleware
    {
        private readonly IAssertorService _assertorService;

        public AssertorMiddleware(IAssertorService assertorService)
            => _assertorService = assertorService;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Task.WaitAll(_assertorService.ValidationInfos
                .Where(x => x.HasMethodAttribute)
                .Select(x => Task.Run(async () =>
                {
                    dynamic service = System.Activator.CreateInstance(x.ServiceType);
                    await service.SetModel(context);
                    if (service.IsModelValid)
                    {
                        await service.ValidateAsync();
                    }
                })).ToArray());

            // try
            // {
            // }
            // catch (ValidationException ex)
            // {
            //     context.Response.StatusCode = ex.StatusCode;
            //     context.Response. = ex.StatusCode;
            //     // throw new MiddlewareException();
            // }

            await next(context);
        }
    }
}
