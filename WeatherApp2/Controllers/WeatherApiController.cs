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
using WeatherApp2.ModelsView;
using System.Threading.Tasks;
using Hangfire;

namespace WeatherApp2.Controllers
{
    public class WeatherApiController : Controller
    {
        public ActionResult GetAllCities()
        {
            List<string> cities = new List<string>();
            cities.Add("Rzeszów");
            cities.Add("Berlin");
            cities.Add("Sosnowiec");
            cities.Add("Warszawa");

            if (cities != null)
            {
                foreach (var city in cities)
                {

                    /*Calling API http://openweathermap.org/api */
                    string apiKey = "7984b03cb110d3afad35dcbc6864a8af";
                    HttpWebRequest apiRequest = WebRequest.Create("http://api.openweathermap.org/data/2.5/weather?q=" + city + "&appid=" + apiKey + "&units=metric") as HttpWebRequest;

                    string apiResponse = "";
                    using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
                    {
                        StreamReader reader = new StreamReader(response.GetResponseStream());
                        apiResponse = reader.ReadToEnd();
                    }



                    ResponseWeather rootObject = JsonConvert.DeserializeObject<ResponseWeather>(apiResponse);

                    WeatherSnap weatherSnap = new WeatherSnap();
                    weatherSnap.Time = DateTime.Now;
                    weatherSnap.CityName = rootObject.name;
                    weatherSnap.Temp = rootObject.main.temp;
                    weatherSnap.Humidity = rootObject.main.humidity;
                    using (var context = new ApplicationDbContext())
                    {
                        context.Weather.Add(weatherSnap);
                        context.SaveChanges();
                    }

                }

            }

            return new EmptyResult();
        }

        public ActionResult Index()
        {
            using (var context = new ApplicationDbContext())
            {
                var cities_data = context.Weather.ToList();
                return View(cities_data);
            }
        }




        //public OpenWeatherMapModels FillCity()
        //{
        //    OpenWeatherMapModels openWeatherMapModels = new OpenWeatherMapModels();
        //    openWeatherMapModels.cities = new Dictionary<string, string>();
        //    openWeatherMapModels.cities.Add("Rzeszów", "Rzeszów");
        //    openWeatherMapModels.cities.Add("Leżajsk", "Leżajsk");
        //    openWeatherMapModels.cities.Add("Warszawa", "Warszawa");
        //    openWeatherMapModels.cities.Add("Sosnowiec", "Sosnowiec");
        //    return openWeatherMapModels;


    }
}
