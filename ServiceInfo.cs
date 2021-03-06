using System;
using System.Collections.Generic;
using System.Linq;

namespace Arcaim.Assertor
{
    public class ValidationInfo
    {
        public Type ServiceType { get; init; }
        public bool HasMethodAttribute { get; init; }

        public ValidationInfo(Type serviceType, IEnumerable<Type> methodParameterTypes)
        {
            var parameterType = serviceType.BaseType.GenericTypeArguments.First();
            HasMethodAttribute = methodParameterTypes.Contains(parameterType);
            ServiceType = serviceType;
        }
    }
}