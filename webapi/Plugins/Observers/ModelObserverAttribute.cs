namespace webapi.Plugins.Observers
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ModelObserverAttribute : Attribute
    {
        public Type Observed { get; }

        public ModelObserverAttribute(Type observed)
        {
            Observed = observed;
        }
    }
}
