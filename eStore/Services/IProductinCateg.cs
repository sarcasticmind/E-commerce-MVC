using eStore.Models;
using System.Collections.Generic;

namespace eStore.Services
{
    public interface IProductinCateg
    {
        List<Product> GetProducts(int? categoryId);
    }
}
