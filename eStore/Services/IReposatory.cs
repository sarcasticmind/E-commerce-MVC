using System.Collections.Generic;

namespace eStore.Services
{
    public interface IReposatory<T>
    {   
        List<T> GetAll(); 
        T GetByID(string id);
        int Delete(string id);
        int Insert(T item);
        int Update(string id, T itemEdit);
    }
}
