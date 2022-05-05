using Arcaim.DI.Scanner;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Arcaim.Assertor;

public static class Extensions
{
  public static IServiceCollection AddAssertor(this IServiceCollection services)
  {
    services.AddSingleton<Arcaim.CQRS.WebApi.Interfaces.IValidator, Validator>();
    services.Scan(a => a.ByAppAssemblies()
      .ImplementationOf(typeof(AbstractValidator<>))
      .WithTransientLifetime());

    return services;
  }
}