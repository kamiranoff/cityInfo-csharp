using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Models
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CityDto
    {

        public int Id { get; set; }

        public String Name { get; set; }

        public String Description { get; set; }

        // Initialize to an empty list to avoid null pointers
        public ICollection<PointOfInterestDto> PointsOfInterest { get; set; } = new List<PointOfInterestDto>();

        public int NumberOfPointsOfInterest { get {
                return PointsOfInterest.Count;
            } 
        }

    }
}
