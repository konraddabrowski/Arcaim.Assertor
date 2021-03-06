using System.Collections.Generic;

namespace Arcaim.Assertor.Interfaces
{
    public interface IAssertorService
    {
        IEnumerable<ValidationInfo> ValidationInfos { get; }
    }
}