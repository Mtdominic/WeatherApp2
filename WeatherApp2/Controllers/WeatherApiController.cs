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
using System.Globalization;

namespace WeatherApp2.Controllers
{
    public class WeatherApiController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        [Authorize]
        public ActionResult GetAllCities()
        {

            List<string> cities = new List<string>();
            cities.Add("Rzeszów,pl");
            cities.Add("Sosnowiec");
            cities.Add("Barcelona");
            cities.Add("Berlin");
            cities.Add("Vieux Lyon");
            cities.Add("Buenos Aires");
            cities.Add("Sidney");
            cities.Add("Tirana");
            cities.Add("Rotterdam");
            cities.Add("Split");
            cities.Add("Sassari");
            cities.Add("Lattes");
            cities.Add("Dabas");
            cities.Add("Kolonie Lerchenau");
            cities.Add("Port Hedland");
            cities.Add("Oslo");
            cities.Add("Leżajsk");
            cities.Add("Madrid");
            cities.Add("Oulu");
            cities.Add("Santiago");
            cities.Add("Adelaide");
            cities.Add("Wellington");
            cities.Add("Nuuk");
            cities.Add("Glasgow");

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

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetData(string SearchString)
        {

            using (var context = new ApplicationDbContext())
            {
                //var cities_data = context.Weather.ToList();



                if (!String.IsNullOrEmpty(SearchString))
                {

                    var cities = context.Weather.Where(q => q.CityName == SearchString).ToList();
                    List<WeatherSnapDto> weatherSnapDtos = new List<WeatherSnapDto>();
                    foreach (var city in cities)
                    {
                        WeatherSnapDto weatherSnapDto = new WeatherSnapDto();
                        weatherSnapDto.Time = city.Time.ToString("HH:mm");
                        weatherSnapDto.Temp = city.Temp;
                        weatherSnapDto.Humidity = city.Humidity;
                        weatherSnapDtos.Add(weatherSnapDto);
                    }

                    var temperatures = weatherSnapDtos.Select(c => c.Temp).ToList();
                    var times = weatherSnapDtos.Select(t => t.Time).ToList();
                    var humidities = weatherSnapDtos.Select(h => h.Humidity).ToList();

                    WeatherSnapJson weatherSnapJson = new WeatherSnapJson();
                    weatherSnapJson.Temps = temperatures;
                    weatherSnapJson.Times = times;
                    weatherSnapJson.Humidities = humidities;
                    
                    return Json(weatherSnapJson, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var cities = context.Weather.ToList();
                    return Json(cities);
                }
            }
        }
    }
}

