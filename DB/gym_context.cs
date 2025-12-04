
using Microsoft.EntityFrameworkCore;

namespace DB
{
    public class gym_context: DbContext
    {
        public gym_context(DbContextOptions<gym_context> options) 
            : base(options)
        { 
            
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Membresias> Membresias { get; set; }

    }
}
