namespace webapi.Plugins;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
public class QueryableVisitorAttribute : Attribute
{
    public Type Visitor { get; }
    public Type Visited { get; }

    public QueryableVisitorAttribute(Type visitor, Type visited)
    {
        Visitor = visitor;
        Visited = visited;
    }
}