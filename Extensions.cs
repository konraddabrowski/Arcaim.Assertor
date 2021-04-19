using Arcaim.CQRS.WebApi.Interfaces;
using Arcaim.DI.Scanner;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Arcaim.Assertor
{
    public static class Extensions
    {
        public static IServiceCollection AddAssertor(this IServiceCollection services)
        {
            services.AddSingleton<IValidatorService, ValidatorService>();
            services.Scan(a => a.ByAppAssemblies()
                .InheritedFrom(typeof(AbstractValidator<>))
                .WithTransientLifetime());

            return services;
        }
    }
}
