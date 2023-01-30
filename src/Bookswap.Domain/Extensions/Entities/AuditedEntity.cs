using Bookswap.Domain.Extensions.Entities.IEntities;

namespace Bookswap.Domain.Extensions.Entities
{
    public abstract class AuditedEntity : IAuditedEntity
    {
        public virtual string? UserCreatorId { get; set; }
        public virtual DateTime CreationDateTime { get; set; }
        public virtual string? LastModifiedUserId { get; set; }
        public virtual DateTime? LastModifiedDateTime { get; set; }
        public virtual bool IsDeleted { get; set; }
    }
}
