using AutoBiographyAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoBiographyAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<UserSavedPics> UserSavedPics { get; set; }
        public DbSet<UserProfile> Users { get; set; }

    }
}
