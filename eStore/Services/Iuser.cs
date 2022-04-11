using System.Collections.Generic;

namespace eStore.Services
{
    public interface Iuser<T>
    {
        List<T> All(string id);
    }
}
