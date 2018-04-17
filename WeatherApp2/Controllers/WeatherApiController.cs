using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WeatherApp2.Models;
using WeatherApp2.Class;


namespace WeatherApp2.Controllers
{
    public class WeatherApiController : Controller
    {
        // GET: OpenWeatherMapMvc
        public ActionResult Index()
        {
            OpenWeatherMapModels openWeatherMapModels = FillCity();
            return View(openWeatherMapModels);
        }

        [HttpPost]
        public ActionResult Index(string cities)
        {
            OpenWeatherMapModels openWeatherMapModels = FillCity();

            if (cities != null)
            {
                /*Calling API http://openweathermap.org/api */
                string apiKey = "7984b03cb110d3afad35dcbc6864a8af";
                HttpWebRequest apiRequest = WebRequest.Create("http://api.openweathermap.org/data/2.5/weather?id=" + cities + "&appid=" + apiKey + "&units=metric") as HttpWebRequest;

                string apiResponse = "";
                using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    apiResponse = reader.ReadToEnd();
                }



                ResponseWeather rootObject = JsonConvert.DeserializeObject<ResponseWeather>(apiResponse);

                StringBuilder sb = new StringBuilder();
                sb.Append("<table><tr><th>Weather Description</th></tr>");
                sb.Append("<tr><td>City:</td><td>" + rootObject.name + "</td></tr>");
                sb.Append("<tr><td>Country:</td><td>" + rootObject.sys.country + "</td></tr>");
                sb.Append("<tr><td>Wind:</td><td>" + rootObject.wind.speed + " Km/h</td></tr>");
                sb.Append("<tr><td>Current Temperature:</td><td>" + rootObject.main.temp + " °C</td></tr>");
                sb.Append("<tr><td>Humidity:</td><td>" + rootObject.main.humidity + "</td></tr>");
                sb.Append("<tr><td>Weather:</td><td>" + rootObject.weather[0].description + "</td></tr>");
                sb.Append("</table>");
                openWeatherMapModels.apiResponse = sb.ToString();
            }
            else
            {
                if (Request.Form["submit"] != null)
                {
                    openWeatherMapModels.apiResponse = "► Select City";
                }
            }
            return View(openWeatherMapModels);
        }

        public OpenWeatherMapModels FillCity()
        {
            OpenWeatherMapModels openWeatherMapModels = new OpenWeatherMapModels();
            openWeatherMapModels.cities = new Dictionary<string, string>();
            openWeatherMapModels.cities.Add("Rzeszów", "7532474");
            openWeatherMapModels.cities.Add("Leżajsk", "7532578");
            openWeatherMapModels.cities.Add("Warszawa", "6695624");
            openWeatherMapModels.cities.Add("Sosnowiec", "7532189");
            return openWeatherMapModels;


        }
    }
}