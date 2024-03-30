namespace PaginatedFilteredProducts.Domain.Common.Abstractions;

public abstract class BaseEntity
{
    public Guid Id { get; }

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType())
            return false;

        var other = (BaseEntity)obj;
        return Id == other.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}