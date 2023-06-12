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
    }
}
