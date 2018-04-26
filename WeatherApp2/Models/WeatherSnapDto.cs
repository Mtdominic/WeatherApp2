using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WeatherApp2.ModelsView
{
    public class WeatherSnapDto
    {
        //[DisplayFormat(DataFormatString = "{0:HH dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public string Time { get; set; }
        public double Temp { get; set; }
        public int Humidity { get; set; }

    }
}