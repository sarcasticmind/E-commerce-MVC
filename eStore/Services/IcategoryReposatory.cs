using System.Collections.Generic;
using System.Linq;
using eStore.Models;

namespace eStore.Services
{

    public class IcategoryReposatory : IRepositoryIntid<Category>, IGetNameReposatory<Category>
    {
        string name;
        eStoreContext context;
        public IcategoryReposatory(eStoreContext _context)
        {
            context = _context;
        }
        public int Delete(int id)
        {
            Category category = GetByID(id);
            context.Categoriesr.Remove(category);
            return context.SaveChanges();
        }

        public List<Category> GetAll()
        {
            return context.Categoriesr.ToList();
        }

        public Category GetByID(int id)
        {
            return context.Categoriesr.FirstOrDefault(c => c.id == id);
        }

        public Category GetByName(string name)
        {
            return context.Categoriesr.FirstOrDefault(c => c.Name == name);
        }

        public int Insert(Category item)
        {
            context.Categoriesr.Add(item);
            return context.SaveChanges();
        }

        public int Update(int id, Category itemEdit)
        {
            var cat = GetByID(id);
            cat.Name = itemEdit.Name;
            return context.SaveChanges();
        }
    }
}
