using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{

    [Route("api/cities")]
    public class PointsOfInterestController : Controller
    {

        [HttpGet("{cityId}/pointsofinterest")]
        public IActionResult GetPointsOfInterest(int cityId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(currentCity => currentCity.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            return Ok(city.PointsOfInterest);
        }

        [HttpGet("{cityId}/pointsofinterest/{id}",Name = "GetPointOfInterest") ]
        public IActionResult GetPointsOfInterest(int cityId, int id)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(currentCity => currentCity.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterest = city.PointsOfInterest.FirstOrDefault(po => po.Id == id);

            if (pointOfInterest == null)
            {
                return NotFound();
            }


            return Ok(pointOfInterest);

        }

        [HttpPost("{cityId}/pointsOfInterest")]
        public IActionResult CreatePointOfInterest(int cityId, [FromBody] PointOfInterestForCreationDto pointOfInterest)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if(pointOfInterest == null || city == null ) {
                return BadRequest();
            }


            // Needs to be improved - Demo purposes.
            var newPointOfInterestId = CitiesDataStore.Current.Cities.SelectMany(c => c.PointsOfInterest)
                .Max(pointsOfInterest => pointsOfInterest.Id);

            var newPointOfInterest = new PointOfInterestDto()
            {
                Id = ++newPointOfInterestId,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description,
            };

            city.PointsOfInterest.Add(newPointOfInterest);

            return CreatedAtRoute("GetPointOfInterest", new { cityId = cityId, id = newPointOfInterestId }, newPointOfInterest);
        }
    }
}