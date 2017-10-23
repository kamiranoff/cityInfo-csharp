using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.API.Models;
using Microsoft.AspNetCore.JsonPatch;
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

        [HttpGet("{cityId}/pointsofinterest/{id}", Name = "GetPointOfInterest")]
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

            if (pointOfInterest == null || city == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
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

            return CreatedAtRoute("GetPointOfInterest", new {cityId = cityId, id = newPointOfInterestId},
                newPointOfInterest);
        }

        [HttpPut("{cityId}/pointsofinterest/{id}")]
        public IActionResult UpdatePointOfInterest(int cityId, int id,
            [FromBody] PointOfInterestForUpdateDto pointOfInterestFromBody)
        {
            if (pointOfInterestFromBody == null)
            {
                return BadRequest();
            }

            if (pointOfInterestFromBody.Name == pointOfInterestFromBody.Description)
            {
                ModelState.AddModelError("Description", "Cannot be identical to Name");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return BadRequest();
            }

            var pointOfInterest = city.PointsOfInterest.FirstOrDefault(p => p.Id == id);

            if (pointOfInterest == null)
            {
                return NotFound();
            }

            pointOfInterest.Name = pointOfInterestFromBody.Name;
            pointOfInterest.Description = pointOfInterestFromBody.Description;

            return NoContent();
        }


        [HttpPatch("{cityId}/pointsofinterest/{id}")]
        public IActionResult PatchPointOfInterest(int cityId, int id,
            [FromBody] JsonPatchDocument<PointOfInterestForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            
            if (city == null)
            {
                return BadRequest();
            }
            
            var pointOfInterest = city.PointsOfInterest.FirstOrDefault(p => p.Id == id);
            
            if (pointOfInterest == null)
            {
                return NotFound();
            }

            
            var pointOfInterestToPatch = new PointOfInterestForUpdateDto()
            {
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description,
            };

            patchDoc.ApplyTo(pointOfInterestToPatch, ModelState);
            TryValidateModel(pointOfInterestToPatch);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            pointOfInterest.Name = pointOfInterestToPatch.Name;
            pointOfInterest.Description = pointOfInterestToPatch.Description;
            
            return CreatedAtRoute("GetPointOfInterest", new {cityId = cityId, id = id},
                pointOfInterest);

            return Ok();

        }

        [HttpDelete("{cityId}/pointsofinterest/{id}")]
        public IActionResult DeletePointOfInterest(int cityId, int id)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            
            if (city == null)
            {
                return BadRequest();
            }
            
            var pointOfInterest = city.PointsOfInterest.FirstOrDefault(p => p.Id == id);
            
            if (pointOfInterest == null)
            {
                return NotFound();
            }

            city.PointsOfInterest.Remove(pointOfInterest);

            return NoContent();

        }
        
    }
}
