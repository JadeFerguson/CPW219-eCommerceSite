using CPW219_eCommerceSite.Models;
using Microsoft.EntityFrameworkCore;

namespace CPW219_eCommerceSite.Data
{
    public class VideoGameContext : DbContext
    {
        // constructor
        public VideoFameContext(DbContextOptions<VideoGameContext> options) : base(options)
        {

        }
        public DbSet<Game> Games { get; set; }
    }
}
