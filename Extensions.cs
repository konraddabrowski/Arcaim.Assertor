using Arcaim.DI.Scanner;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Arcaim.Assertor
{
    public static class Extensions
    {
        public static IServiceCollection AddAssertor(this IServiceCollection services)
        {
            services.AddSingleton<IAssertor, Assertor>();
            services.Scan(a => a.ByAppAssemblies()
                .InheritFrom(typeof(AbstractValidator<>))
                .WithTransientLifetime());

            return services;
        }
    }
}