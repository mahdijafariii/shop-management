using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Newtonsoft.Json;
using online_shop.Validator;

namespace online_shop.Controller;

[ApiController]
[Route("api/[controller]")]
public class LocationController : ControllerBase
{
    [HttpGet("get-province")]
    public async Task<IActionResult> GetProvinces()
    {
        var provincePath = Path.Combine(Directory.GetCurrentDirectory(), "../online shop/cities/provinces.json");

        if (!System.IO.File.Exists(provincePath))
        {
            return NotFound("File not found.");
        }

        var jsonContent = await System.IO.File.ReadAllTextAsync(provincePath);
        return Content(jsonContent, "application/json");
    }
    
    [HttpGet("get-cities")]
    public async Task<IActionResult> GetCities([FromQuery] int provinceId)
    {
        var provincePath = Path.Combine(Directory.GetCurrentDirectory(), "../online shop/cities/provinces.json");
        if (!System.IO.File.Exists(provincePath))
        {
            return NotFound("File not found.");
        }
        var provinceJsonContent = await System.IO.File.ReadAllTextAsync(provincePath);
        var provinces = JsonConvert.DeserializeObject<List<Provinces>>(provinceJsonContent);
        var provinceSearch = provinces.FirstOrDefault(u => u.id == provinceId);
        if (provinceSearch is null)
        {
            return NotFound("Province with this id not found !!");
        }
        var citiesPath = Path.Combine(Directory.GetCurrentDirectory(), "../online shop/cities/cities.json");
        if (!System.IO.File.Exists(citiesPath))
        {
            return NotFound("File not found.");
        }
        var citiesJsonContent = await System.IO.File.ReadAllTextAsync(citiesPath);
        var cities = JsonConvert.DeserializeObject<List<Cities>>(citiesJsonContent);
        var citiesInProvince = cities.Where(u => u.province_id == provinceSearch.id);
        return Content(citiesInProvince.ToJson(), "application/json");
    }
}