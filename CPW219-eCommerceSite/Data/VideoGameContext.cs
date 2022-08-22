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
        public DbSet<Member> Members { get; set; }
    }
}
