namespace SavingsBook.Application.Contracts.Common;

public abstract class AuditedEntityDto<T>
{
    public DateTime CreationTime { get; set; }
    public T? CreatorId { get; set; }
    public DateTime? LastModificationTime { get; set; }
    public bool IsActive { get; set; }
    public T? LastModifierId { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletionTime { get; set; }
    public Guid? DeleterId { get; set; }
    public T? Id { get; set; }
}