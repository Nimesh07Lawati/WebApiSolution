using Microsoft.EntityFrameworkCore;
using WebApiPractice.Models.Domain;

namespace WebApiPractice.Data
{
    public class WebApiPracticeDbContext:DbContext
    {
        public WebApiPracticeDbContext(DbContextOptions<WebApiPracticeDbContext> dbContextOptions): base(dbContextOptions)
        {
            
        }
        public DbSet<Difficulty> Difficulty { get; set; }
      
        public DbSet <Region> Region { get; set; }
        public DbSet<Walk> Walk { get; set; }
    }
}
