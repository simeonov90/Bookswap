using Bookswap.Domain.Extensions.Entities.IEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace Bookswap.Domain.DbContext
{
    public class BookswapDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public BookswapDbContext(DbContextOptions<BookswapDbContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        //add DbSet entity models 

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
