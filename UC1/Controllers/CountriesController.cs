using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using UC1.Models;

namespace UC1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private IConfiguration _configuration { get; }
        private readonly string _externalApiUrl;


        public CountriesController(IConfiguration configuration)
        {
            _configuration = configuration;
            _externalApiUrl = _configuration.GetValue<string>("Urls:CountriesApiUrl");
        }

        [HttpGet]
        [Route("")]
        public async Task<object> Get(string? param1, int? param2, string? param3)
        {
            object? apiResponse = null;
            using (var client = new HttpClient())
            {
                apiResponse = await client.GetFromJsonAsync(_externalApiUrl, typeof(List<Country>));
            }
            return Ok(apiResponse);
        }

        #region Filters

        /// <summary>
        /// Filters countries by name.
        /// </summary>
        /// <param name="searchString">Part of the name.</param>
        /// <param name="countries">Reference to countries collection.</param>
        private static void filterByName(string searchString, ref IEnumerable<Country> countries)
        {
            countries = countries.Where(c => !string.IsNullOrEmpty(c.Name?.Common) ? c.Name.Common.Contains(searchString, StringComparison.InvariantCultureIgnoreCase) : false);
        }

        /// <summary>
        /// Filters countries by population. Countries where the population is less than provided number are shown in result set.
        /// </summary>
        /// <param name="population">Population number in the millions of people .</param>
        /// <param name="countries">Reference to countries collection.</param>
        private static void filterByPopulation(int population, ref IEnumerable<Country> countries)
        {
            countries = countries.Where(c => c.Population < population * 1000000);
        }
        #endregion
    }
}
