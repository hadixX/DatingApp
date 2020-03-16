using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Controllers.models
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options){}

        public DbSet<value> Values {get;set;}
    }
}