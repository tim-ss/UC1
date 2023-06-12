using System.Runtime.Serialization;

public class Country
{
    [DataMember(Name = "name")]
    public CountryName? Name { get; set; }

    [DataMember(Name = "region")]
    public string? Region { get; set; }

    [DataMember(Name = "subregion")]
    public string? Subregion { get; set; }

    [DataMember(Name = "population")]
    public int? Population { get; set; }

    [DataMember(Name = "languages")]
    public Dictionary<string, string>? Languages { get; set; }

    [DataMember(Name = "currencies")]
    public Dictionary<string, Currency>? Currencies { get; set; }
}