using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using UC1.Models;

[assembly: InternalsVisibleTo("UC1.Tests")]
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

        /// <summary>
        /// Returns the list of countries.
        /// </summary>
        /// <param name="searchString">Country name search string.</param>
        /// <param name="population">Population number in the millions.</param>
        /// <param name="sortDirection">Sorting direction. Possible values "ascend" or "descend".</param>
        /// <param name="pageSize">Page size for limiting the result set.</param>
        /// <returns>Response containing the list of countries.</returns>
        [HttpGet]
        [Route("")]
        public async Task<ObjectResult> Get(string? searchString, int? population, string? sortDirection, int? pageSize)
        {
            IEnumerable<Country>? countries = null;
            using (var client = new HttpClient())
            {
                countries = await client.GetFromJsonAsync<IEnumerable<Country>>(_externalApiUrl);
            }

            if (countries != null)
            {
                if (!string.IsNullOrEmpty(searchString))
                {
                    filterByName(searchString, ref countries);
                }
                if (population.HasValue)
                {
                    filterByPopulation(population.Value, ref countries);
                }
                if (!string.IsNullOrEmpty(sortDirection))
                {
                    sortByCountryName(sortDirection, ref countries);
                }
                if (pageSize.HasValue)
                {
                    paginate(pageSize.Value, ref countries);
                }
            }

            return Ok(countries);
        }

        #region Filters

        /// <summary>
        /// Filters countries by name.
        /// </summary>
        /// <param name="searchString">Part of the name.</param>
        /// <param name="countries">Reference to countries collection.</param>
        internal protected static void filterByName(string searchString, ref IEnumerable<Country> countries)
        {
            countries = countries.Where(c => !string.IsNullOrEmpty(c.Name?.Common) && c.Name.Common.Contains(searchString, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// Filters countries by population. Countries where the population is less than provided number are shown in result set.
        /// </summary>
        /// <param name="population">Population number in the millions of people.</param>
        /// <param name="countries">Reference to countries collection.</param>
        internal protected static void filterByPopulation(int population, ref IEnumerable<Country> countries)
        {
            long populationNumber = (long)population * 1000000;
            countries = countries.Where(c => c.Population < populationNumber);
        }

        /// <summary>
        /// Sorts countries by name.
        /// </summary>
        /// <param name="direction">Sorting direction. Possible values "ascend" or "descend".</param>
        /// <param name="countries">Reference to countries collection.</param>
        internal protected static void sortByCountryName(string direction, ref IEnumerable<Country> countries)
        {
            switch (direction)
            {
                case "ascend":
                    countries = countries.OrderBy(c => c.Name?.Common);
                    break;
                case "descend":
                    countries = countries.OrderByDescending(c => c.Name?.Common);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Limits the amount of data in results set.
        /// </summary>
        /// <param name="direction">Number of record to retreive.</param>
        /// <param name="countries">Reference to countries collection.</param>
        internal protected static void paginate(int number, ref IEnumerable<Country> countries)
        {
            countries = countries.Take(number);
        }

        #endregion
    }
}
