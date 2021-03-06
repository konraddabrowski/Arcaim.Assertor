using System.Threading.Tasks;
using Arcaim.Assertor.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Arcaim.Assertor
{
    public class AssertorDispatcher : IAssertorDispatcher
    {
        private readonly IServiceScopeFactory _serviceFactory;

        public AssertorDispatcher(IServiceScopeFactory serviceFactory)
            => _serviceFactory = serviceFactory;

        public async Task ValidateAsync<T>(T model)
        {
            using var scope = _serviceFactory.CreateScope();
            var handler = scope.ServiceProvider.GetRequiredService<AbstractAssertor<T>>();
            await handler.ValidateAsync(model);
        }
    }
}