using Microsoft.AspNetCore.Http;

namespace Arcaim.Assertor.Exceptions
{
    public class AssertorServiceException : ValidationException
    {
        public override string Code => "assertor_service";
        public override int StatusCode => StatusCodes.Status500InternalServerError;

        private AssertorServiceException(string serviceName) : base($"Service type of {serviceName} not exists")
        {
        }

        public static AssertorServiceException Create(string serviceName)
            => new AssertorServiceException(serviceName);
    }
}