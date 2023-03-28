using AccountingWebApp;
using AccountingWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountingWebApp.Data
{
    public class AplicationDBContext : DbContext
    {
        public AplicationDBContext(DbContextOptions<AplicationDBContext> option) : base(option)
        {

        }

        public DbSet<Account> Accounting { get; set; }
    }
}
