namespace webapi.Model.BaseEntities;

public interface INamedModel : IIdentityModel
{
    string Name { get; set; }
}

public abstract class NamedModel<T> : BaseIdentityModel<T>, INamedModel, IIdentityModel, IEquatable<T> where T : class, INamedModel, IIdentityModel
{
    public virtual string Name { get; set; } = default!;

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals(obj as T);
    }

    public bool Equals(T? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;

        return base.Equals(other) || Name == other.Name;
    }

    public override int GetHashCode()
    {
        return -1;
    }
}
