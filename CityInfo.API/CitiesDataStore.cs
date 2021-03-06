﻿using System;
using System.Collections.Generic;
using CityInfo.API.Models;

namespace CityInfo.API
{
    public class CitiesDataStore
    {

        public static CitiesDataStore Current { get; } = new CitiesDataStore();

        public List<CityDto> Cities { get; set; }

        public CitiesDataStore()
        {
            Cities = new List<CityDto> {
                new CityDto() {
                    Id = 1,
                    Name="New york City",
                    Description="Big Apple",
                    PointsOfInterest= new List<PointOfInterestDto>() {
                        new PointOfInterestDto() {
                            Id=1,
                            Name="Central Park",
                            Description="The most visited park"
                        },
                        new PointOfInterestDto() {
                            Id=2,
                            Name="Empire State building",
                            Description="A 102-story skyscraper"
                        }
                    }

                },
                new CityDto() {
                    Id = 2,
                    Name="London",
                    Description="Small Apple",

                },
                new CityDto() {
                    Id = 3,
                    Name="Miami",
                    Description="Sunshine State Party city",

                },
            };
        }

    }
}
