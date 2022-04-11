using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using eStore.Models;

namespace eStore.Services
{
    public class IproductReposatory : IRepositoryIntid<Product>, IGetNameReposatory<Product>
        ,IProductinCateg
    {
        eStoreContext context;
        public IproductReposatory(eStoreContext _context)
        {
            context = _context; 
        }
        public int Delete(int id)
        {
            Product product = GetByID(id);
            context.Products.Remove(product);
            return context.SaveChanges();
        }

        public List<Product> GetAll()
        {
            return context.Products.Include(n => n.Cat).ToList();
        }

        public Product GetByID(int id)
        {
            return context.Products.Include(n=>n.Cat).FirstOrDefault(n=>n.id==id);
        }

        public Product GetByName(string name)
        {
            return context.Products.FirstOrDefault(n=>n.Name == name);
        }

        public List<Product> GetProducts(int? categoryId)
        {
            var products = context.Products.Where(n => n.Cat_id == categoryId).ToList();
            return products;
        }

        public int Insert(Product item)
        {
            context.Products.Add(item);
            return context.SaveChanges();
        }

        public int Update(int id, Product itemEdit)
        {
            Product product = GetByID(id);
            product.Name = itemEdit.Name;
            product.Price = itemEdit.Price;
            product.Image1 = itemEdit.Image1;
            product.Image2 = itemEdit.Image2;
            product.Image3 = itemEdit.Image3;
            product.Cat_id = itemEdit.Cat_id;
            return context.SaveChanges();
        }
    }
}
