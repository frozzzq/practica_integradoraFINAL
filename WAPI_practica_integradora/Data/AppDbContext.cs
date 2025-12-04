using DB;
using Microsoft.EntityFrameworkCore;

namespace WAPI_practica_integradora.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
    }
}
