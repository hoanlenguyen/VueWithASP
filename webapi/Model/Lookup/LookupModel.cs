namespace webapi.Model.Lookup
{
    public interface ILookupModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string[] Keys { get; set; }
    }

    public class LookupModel : ILookupModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string[] Keys { get; set; } = new Hashset<string>();
    }
}