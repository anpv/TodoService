namespace ToDo.Domain.SeedWork;

public abstract class Entity
{
    public long Id { get; }

    public bool IsTransient() => Id == default;

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        var entity = (Entity)obj;

        return !IsTransient() && !entity.IsTransient() && Id == entity.Id;
    }

    public override int GetHashCode() => IsTransient() ? base.GetHashCode() : HashCode.Combine(Id);

    public static bool operator ==(Entity? left, Entity? right) => left?.Equals(right) ?? Equals(right, null);

    public static bool operator !=(Entity? left, Entity? right) => !(left == right);
}
