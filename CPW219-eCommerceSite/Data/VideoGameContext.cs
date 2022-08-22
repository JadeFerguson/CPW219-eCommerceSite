using CPW219_eCommerceSite.Models;
using Microsoft.EntityFrameworkCore;

namespace CPW219_eCommerceSite.Data
{
    public class VideoGameContext : DbContext
    {
        // constructor
        public VideoGameContext(DbContextOptions<VideoGameContext> options) : base(options)
        {

        }
        public DbSet<Game> Games { get; set; }

        // If want to add to the database you have to add it as a property as a Dbset in the database
        // Then you want to add migration of new one by doing Add-Migration (name of want) in nuget package consols
        // then do Update-database
        public DbSet<Member> Members { get; set; }
    }
}
