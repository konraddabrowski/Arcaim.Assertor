using System.Threading.Tasks;
using Arcaim.Assertor.Exceptions;
using Arcaim.CQRS.WebApi.Interfaces;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Arcaim.Assertor
{
    public class ValidatorService : IValidatorService
    {
        private readonly IServiceScopeFactory _serviceFactory;

        public ValidatorService(IServiceScopeFactory serviceFactory)
            => _serviceFactory = serviceFactory;

        public async Task ValidateAsync<T>(T instance)
        {
            using var scope = _serviceFactory.CreateScope();
            var validator = scope.ServiceProvider.GetService<AbstractValidator<T>>();
            if (validator is null)
            {
                throw AssertorServiceException.Create(nameof(AbstractValidator<T>));
            }
            await validator.ValidateAndThrowAsync(instance);
        }
    }
}