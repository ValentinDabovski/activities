namespace Domain.Common;

public abstract class Entity
{
    protected Entity(Guid? id)
    {
        Id = id ?? Guid.NewGuid();
    }

    protected Entity()
    {
    }

    public Guid Id { get; set; }

    public override bool Equals(object obj)
    {
        if (obj is null || obj.GetType() != GetType()) return false;

        return ((Entity)obj).Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}