using Catstagram.Server.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Catstagram.Server.Data
{
    public class CatstagramDbContext : IdentityDbContext<User>
    {
        public CatstagramDbContext(DbContextOptions<CatstagramDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cat> Cats => Set<Cat>();
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
            .Entity<Cat>()
            .HasOne(c => c.User)
            .WithMany(u => u.Cats)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}