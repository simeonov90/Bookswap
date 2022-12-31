using Bookswap.Domain.Extensions.Entities.IEntities;
using Bookswap.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace Bookswap.Domain.DbContext
{
    public class BookswapDbContext : IdentityDbContext<BookswapUser, IdentityRole, string>
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public BookswapDbContext(
            DbContextOptions<BookswapDbContext> options, 
            IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        //add DbSet entity models 
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<IAuditedEntity>().Where(e => 
            e.State == EntityState.Added ||
            e.State == EntityState.Modified ||
            e.State == EntityState.Deleted);

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreationDateTime = DateTime.Now;
                    entry.Entity.UserCreatorId = httpContextAccessor?.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.LastModifiedDateTime = DateTime.Now;
                    entry.Entity.LastModifiedUserId = httpContextAccessor?.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
                }

                if (entry.State == EntityState.Deleted)
                {
                    entry.Entity.IsDeleted = true;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
