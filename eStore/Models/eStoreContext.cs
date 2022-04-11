using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eStore.Models
{
    public class eStoreContext: IdentityDbContext<AccountUser>
    {
        public eStoreContext()
        {
        }
        public eStoreContext(DbContextOptions options) : base(options)
        { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categoriesr { get; set; }
        public DbSet<ProductOrder> productOrders { get; set; }
        public DbSet<AccountUser> AccountUsers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=eStoreDb;Integrated Security=True");
        }
    }
}
