using Core.Entities;

namespace Entities.Concrete.Common;

public abstract class BaseEntity : IEntity
{
    public Guid Id { get; set; }
    public virtual DateTime CreatedDate { get; set; }
    public virtual DateTime? UpdatedDate { get; set; }
    public virtual DateTime? DeletedDate { get; set; }
    public virtual bool IsDeleted { get; set; }
}
