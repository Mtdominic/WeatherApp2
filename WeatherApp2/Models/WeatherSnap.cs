using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WeatherApp2.ModelsView
{
    public class WeatherSnap
    {
        public int Id { get; set; }
        [Display(Name = "City: ")]
        public string CityName { get; set; }
        [Display(Name = "Temperature: ")]
        public double Temp { get; set; }
        [Display(Name = "Humidity: ")]
        public int Humidity { get; set; }
    }
}