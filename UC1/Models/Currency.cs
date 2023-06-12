using System.Runtime.Serialization;
using System.Xml.Linq;

public class Currency
{
    [DataMember(Name = "name")]
    public string? Name { get; set; }

    [DataMember(Name = "symbol")]
    public string? Symbol { get; set; }
}

