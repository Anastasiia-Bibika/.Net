using Microsoft.EntityFrameworkCore;
using Laboratory2.Models;
namespace Laboratory2.Data
{
    public class AccountContext: DbContext
    {
        public AccountContext(DbContextOptions<AccountContext> options) : base(options) { }

        public DbSet<Account> Account { get; set; }
    }
}
