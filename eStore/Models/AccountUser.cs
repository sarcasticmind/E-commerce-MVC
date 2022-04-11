using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace eStore.Models
{
    public class AccountUser:IdentityUser
    {
        public virtual List<ProductOrder> ProductOrders { get; set; }
    }
}
