using Microsoft.EntityFrameworkCore;
using MVCCoreCF.Models;

namespace MVCCoreCF.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }
        public DbSet<Products> products { get; set; }
        public DbSet<Category> categories { get; set; }
    }
}
