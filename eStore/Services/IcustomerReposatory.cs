using System.Collections.Generic;
using System.Linq;
using eStore.Models;

namespace eStore.Services
{
    public class IcustomerReposatory : IReposatory<AccountUser>, IGetNameReposatory<AccountUser>
    {
        eStoreContext context;
        public IcustomerReposatory(eStoreContext _context)
        {
            context = _context;
        }
        public int Delete(string id)
        {
            AccountUser cust = GetByID(id);
            context.AccountUsers.Remove(cust);
            return context.SaveChanges();
        }

        public List<AccountUser> GetAll()
        {
            return context.AccountUsers.ToList();
        }

        public AccountUser GetByID(string id)
        {
            return context.AccountUsers.FirstOrDefault(n => n.Id == id);
        }

        public AccountUser GetByName(string username)
        {
            return context.AccountUsers.FirstOrDefault(n => n.UserName == username);
        }

        public int Insert(AccountUser item)
        {
           context.AccountUsers.Add(item);
            return context.SaveChanges();
        }

        public int Update(string id, AccountUser itemEdit)
        {
            AccountUser customer = GetByID(id);
            customer.UserName = itemEdit.UserName;
            customer.Email = itemEdit.Email;
            customer.PasswordHash = itemEdit.PasswordHash;
            return context.SaveChanges();

        }
    }
}
