﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WeatherApp2.ModelsView
{
    public class WeatherSnap
    {
        public int Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:HH dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Time { get; set; }
        [Display(Name = "City: ")]
        public string CityName { get; set; }
        [Display(Name = "Temperature: ")]
        public double Temp { get; set; }
        [Display(Name = "Humidity: ")]
        public int Humidity { get; set; }
    }
}