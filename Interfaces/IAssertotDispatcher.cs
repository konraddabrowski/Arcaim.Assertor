using System.Threading.Tasks;

namespace Arcaim.Assertor.Interfaces
{
    public interface IAssertorDispatcher
    {
        Task ValidateAsync<T>(T model);
    }
}
