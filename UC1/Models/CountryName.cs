using System.Runtime.Serialization;

namespace UC1.Models
{
    public class CountryName
    {
        [DataMember(Name = "common")]
        public string? Common { get; set; }

        [DataMember(Name = "official")]
        public string? Official { get; set; }
    }
}