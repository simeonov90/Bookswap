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
    public class BookswapDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ILogger logger;

        public BookswapDbContext(
            DbContextOptions<BookswapDbContext> options, 
            IHttpContextAccessor httpContextAccessor,
            ILogger logger)
            : base(options)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.logger = logger;
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
            var entries = ChangeTracker.Entries().Where(e => e.Entity is IAuditedEntity &&
            e.State == EntityState.Added ||
            e.State == EntityState.Modified ||
            e.State == EntityState.Deleted);

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    ((IAuditedEntity)entry.Entity).CreationDateTime = DateTime.Now;
                    ((IAuditedEntity)entry.Entity).UserCreatorId = httpContextAccessor?.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
                }

                if (entry.State == EntityState.Modified)
                {
                    ((IAuditedEntity)entry.Entity).LastModifiedDateTime = DateTime.Now;
                    ((IAuditedEntity)entry.Entity).LastModifiedUserId = httpContextAccessor?.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
                }

                if (entry.State == EntityState.Deleted)
                {
                    ((IAuditedEntity)entry.Entity).IsDeleted = true;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
