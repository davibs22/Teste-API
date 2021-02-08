using Microsoft.EntityFrameworkCore;

namespace ClientApi.Models
{
    public class ClientContext : DbContext
    {
        public ClientContext (DbContextOptions<ClientContext> options)
            : base(options)
        {
        }

        public DbSet<ClientItem> ClientItems { get; set; }
    }
}
