using System.Linq;
using System.Threading.Tasks;
using Arcaim.Assertor.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Arcaim.Assertor
{
    public class AssertorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IAssertorService _assertorService;

        public AssertorMiddleware(RequestDelegate next, IAssertorService assertorService)
            => (_next, _assertorService) = (next, assertorService);

        public async Task Invoke(HttpContext context)
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

            await _next(context);
        }
    }
}
