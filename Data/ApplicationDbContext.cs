using Microsoft.EntityFrameworkCore;
using MyRoof.WASM.Models;

namespace Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        
        public DbSet<Product> Products { get; set; }    



    }
}
