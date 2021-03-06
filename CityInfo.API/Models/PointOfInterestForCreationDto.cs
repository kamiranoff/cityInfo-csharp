﻿using System;
using System.ComponentModel.DataAnnotations;

namespace CityInfo.API.Models
{
    public class PointOfInterestForCreationDto
    {
        [Required(ErrorMessage = "Provide a value.")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Provide a value.")]
        [MaxLength(200)]
        public string Description { get; set; }
    }
}

