using System.Threading.Tasks;

namespace Arcaim.Assertor
{
    public interface IAssertor
    {
        Task ValidateAsync<T>(T model);
    }
}
