using System.Collections.Generic;

namespace webapi.Model.Lookup
{
    public interface ILookupModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Keys { get; set; }
    }

    public class LookupModel : ILookupModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Keys { get; set; } = new HashSet<string>();
    }
}