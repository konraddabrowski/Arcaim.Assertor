using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Arcaim.Assertor
{
    public class Assertor : IAssertor
    {
        private readonly IServiceScopeFactory _serviceFactory;

        public Assertor(IServiceScopeFactory serviceFactory)
            => _serviceFactory = serviceFactory;

        public async Task ValidateAsync<T>(T model)
        {
            using var scope = _serviceFactory.CreateScope();
            var handler = scope.ServiceProvider.GetRequiredService<AbstractValidator<T>>();
            await handler.ValidateAsync(model);
        }
    }
}