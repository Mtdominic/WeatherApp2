using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherApp2.Models
{
    public class OpenWeatherMapModels
    { 
    public string apiResponse { get; set; }
    public Dictionary<string, string> cities { get; set; }
    }
}