using Microsoft.AspNetCore.Mvc;
using TMS.Data.Interfaces;

namespace TMS.Controllers
{
    public class CityController : ControllerBase
    {
        public readonly ICityRepository _cityRepository;
        public CityController(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }


        [HttpGet("api/cities/{id}")]
        public IActionResult GetCityById(int id)
        {
            var city = _cityRepository.GetCityById(id);
            if (city == null)
            {
                return NotFound();
            }
            return Ok(city);
        }
    }
}
