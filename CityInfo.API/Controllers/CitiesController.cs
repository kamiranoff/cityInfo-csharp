using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{

    [Route("api/cities")]
    public class CitiesController : Controller
    {
        public CitiesController() { }


        [HttpGet()]
        public IActionResult GetCities()
        {
            var cities = CitiesDataStore.Current.Cities;

            if (cities == null)
            {
                return NotFound();
            }

            return Ok(cities);

        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(cities => cities.Id == id);
            if (city == null)
            {
                return NotFound();
            }
            return Ok(city);
        }
    }
}
