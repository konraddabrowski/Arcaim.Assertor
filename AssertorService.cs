using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Arcaim.Assertor.Interfaces;
using FluentValidation;

namespace Arcaim.Assertor
{
    public class AssertorService : IAssertorService
    {
        private readonly HashSet<ValidationInfo> _serviceInfos = new HashSet<ValidationInfo>();
        public IEnumerable<ValidationInfo> ValidationInfos { get => _serviceInfos; }

        public AssertorService()
        {
            var assemblies = Directory
                .GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
                .Select(x => Assembly.Load(AssemblyName.GetAssemblyName(x)));

            var methodParameterTypes = assemblies.SelectMany(assembly => assembly.GetTypes().SelectMany(type => type.GetMethods()))
                .Where(method => method.GetCustomAttribute<ValidateAttribute>() is not null)
                .SelectMany(methodInfo => methodInfo.GetParameters()).Select(x => x.ParameterType);

            var assertorTypes = assemblies.SelectMany(assembly => assembly.GetTypes()).Where(type =>
                type.IsClass &&
                !type.IsAbstract &&
                !type.IsInterface &&
                !type.IsGenericType &&
                type.BaseType.Name.Equals(typeof(AbstractValidator<>).Name, StringComparison.Ordinal));


            assertorTypes.ToList().ForEach(serviceType
                => _serviceInfos.Add(new ValidationInfo(serviceType, methodParameterTypes)));
        }
    }
}