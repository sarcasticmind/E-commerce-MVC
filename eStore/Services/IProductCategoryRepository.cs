using System.Collections.Generic;
using eStore.Models;

namespace eStore.Services
{
    public interface IProductCategoryRepository
    {
        List<Product> GetRelation();
    }
}
