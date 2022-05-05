using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Arcaim.Assertor;

public class Validator : Arcaim.CQRS.WebApi.Interfaces.IValidator
{
  private readonly IServiceScopeFactory _serviceFactory;

  public Validator(IServiceScopeFactory serviceFactory)
    => _serviceFactory = serviceFactory;

  public async Task ValidateAsync<T>(T instance)
  {
    using var scope = _serviceFactory.CreateScope();
    var validator = scope.ServiceProvider.GetService<AbstractValidator<T>>();
    if (validator is not null)
    {
      await validator.ValidateAsync(instance);
    }
  }
}