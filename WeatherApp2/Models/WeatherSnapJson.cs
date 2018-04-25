using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WeatherApp2.ModelsView
{
    public class WeatherSnapJson
    {
       
        public List<string> Times { get; set; }
        public List<double> Temps { get; set; }
        public List<int> Humidities { get; set; }

    }
}