using System.Collections.Generic;

namespace eStore.Services
{
    
        public interface IRepositoryIntid<T>
        {
            List<T> GetAll();
            T GetByID(int id);
            int Delete(int id);
            int Insert(T item);
            int Update(int id, T itemEdit);
        }
    
}
