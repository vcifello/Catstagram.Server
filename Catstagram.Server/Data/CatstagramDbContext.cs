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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
            .Entity<Cat>()
            .HasOne(c => c.User)
            .WithMany(u => u.Cats)
            .HasForeignKey(c=>c.UserId)
            .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}