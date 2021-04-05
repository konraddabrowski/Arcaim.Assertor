namespace Arcaim.Assertor.Exceptions
{
    public abstract class ValidationException : System.Exception
    {
        public abstract string Code { get; }
        public abstract int StatusCode { get; }

        public ValidationException(string message) : base(message)
        {
        }
    }
}