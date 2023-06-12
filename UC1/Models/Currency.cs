using System.Runtime.Serialization;
using System.Xml.Linq;

namespace UC1.Models
{
    public class Currency
    {
        [DataMember(Name = "name")]
        public string? Name { get; set; }

        [DataMember(Name = "symbol")]
        public string? Symbol { get; set; }
    }
}
