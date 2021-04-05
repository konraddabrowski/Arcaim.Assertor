using Arcaim.Assertor.Interfaces;
using Arcaim.DI.Scanner;
using Microsoft.Extensions.DependencyInjection;

namespace Arcaim.Assertor
{
    public static class Extensions
    {
        public static IServiceCollection AddAssertor(this IServiceCollection services)
        {
            services.Scan(a => a.ByAppAssemblies()
                .InheritedFrom(typeof(AbstractValidator<>))
                .WithTransientLifetime());
            services.AddSingleton<IAssertorService, AssertorService>();

            return services;
        }

        public static void Validate<T>(this IValidator validator)
            => validator.Validate();
    }
}
