namespace Bookswap.Domain.Extensions.Entities.IEntities
{
    public interface IAuditedEntity
    {
        public string? UserCreatorId { get; set; }
        public DateTime CreationDateTime { get; set; }
        public string? LastModifiedUserId { get; set; }
        public DateTime? LastModifiedDateTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
