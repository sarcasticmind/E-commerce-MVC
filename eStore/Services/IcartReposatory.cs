using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using eStore.Models;

namespace eStore.Services
{
    public class IcartReposatory : IRepositoryIntid<ProductOrder> , Iuser<ProductOrder>
    {
        eStoreContext context;
        public IcartReposatory(eStoreContext _context)
        {
            context = _context;
        }

        public List<ProductOrder> All(string id)
        {
            return context.productOrders.Where(n=>n.Customer_id ==id && n.isCheckedout == false).Include(n=>n.Product).Include(n=>n.customer).ToList();
        }

        public int Delete(int id)
        {
            var cart = GetByID(id);
            context.productOrders.Remove(cart);
            return context.SaveChanges();
        }

        public List<ProductOrder> GetAll()
        {
            return context.productOrders.Include(n=>n.Product).Include(n=>n.customer).ToList();
        }

        public ProductOrder GetByID(int id)
        {
            return context.productOrders.SingleOrDefault(n=>n.id==id);
        }

        public int Insert(ProductOrder item)
        {
            context.productOrders.Add(item);
            return context.SaveChanges();
        }

        public int Update(int id, ProductOrder itemEdit)
        {
            var cart = GetByID(id);
            cart.Quantity = itemEdit.Quantity;
            cart.isCheckedout = itemEdit.isCheckedout;
            cart.Date = itemEdit.Date;
            cart.Price = itemEdit.Price;
            return context.SaveChanges();
        }
    }
}
