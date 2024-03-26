using Microsoft.EntityFrameworkCore;
using StackOverflow.Models.Entities;

namespace StackOverflow.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            
        }

        public DbSet<Votes> Votes { get; set; }
    }
}
