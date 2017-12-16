using Microsoft.EntityFrameworkCore;
 
namespace belt.Models
{
    public class MainContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public MainContext(DbContextOptions<MainContext> options) : base(options) { }
        public DbSet<User> user { get; set;}

        public DbSet<Activity> activity { get; set;}

        public  DbSet<Participant> participant {get; set;}
    
    }
}