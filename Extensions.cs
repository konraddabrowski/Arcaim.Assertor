using Arcaim.Assertor.Interfaces;
using Arcaim.DI.Scanner;
using Microsoft.Extensions.DependencyInjection;

namespace Arcaim.Assertor
{
    public static class Extensions
    {
        public static IServiceCollection AddAssertor(this IServiceCollection services)
        {
            services.AddSingleton<IAssertorDispatcher, AssertorDispatcher>();
            services.Scan(a => a.ByAppAssemblies()
                .InheritedFrom(typeof(AbstractAssertor<>))
                .WithTransientLifetime());
            services.AddSingleton<IAssertorService, AssertorService>();

            return services;
        }
    }
}
