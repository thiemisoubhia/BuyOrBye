using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BuyOrBye.Models;

namespace BuyOrBye.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<BuyOrBye.Models.Product> Product { get; set; } = default!;
        public DbSet<BuyOrBye.Models.Review> Review { get; set; } = default!;
    }
}
