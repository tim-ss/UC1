using Microsoft.AspNetCore.Mvc;

namespace UC1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private const string externalApiUrl = "https://restcountries.com/v3.1/all";
        
        [HttpGet]
        [Route("")]
        public async Task<object> Get(string? param1, int? param2, string? param3)
        {
            object? apiResponse = null;
            using (var client = new HttpClient())
            {
                apiResponse = await client.GetFromJsonAsync(externalApiUrl, typeof(List<Country>));
            }
            return Ok(apiResponse);
        }
    }
}
