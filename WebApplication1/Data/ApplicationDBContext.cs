using Microsoft.EntityFrameworkCore; 
using WebApplication1.Data.Entities;

namespace WebApplication1.Data
{
    public class ApplicationDBContext: DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {

        }
        public DbSet<CategoryEntity> Categories { get; set; }
    }
}
