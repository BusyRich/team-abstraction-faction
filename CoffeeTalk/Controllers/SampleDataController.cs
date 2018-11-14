using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeTalk.Services; //Imports DB Service
using CoffeeTalk.Models; //Imports the Coffee Model
using Microsoft.AspNetCore.Mvc;

namespace CoffeeTalk.Controllers
{

    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {

        private DBServices DB = new DBServices(); //instantiates the MySQL DB object

        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet("[action]")]
        public IEnumerable<WeatherForecast> WeatherForecasts()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }

        public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }

            public int TemperatureF
            {
                get
                {
                    return 32 + (int)(TemperatureC / 0.5556);
                }
            }
        }

        //returns a list of coffee objects
        [HttpGet("[action]")]
        public List<Coffee> GetCoffee()
        {
            return DB.Select();
        }

        //creates a new coffee on the database
        [HttpPost]
        public IActionResult CreateCoffee([FromBody]Coffee coffee)
        {
            if (DB.Insert(coffee))
                return StatusCode(200, "Your coffee was added to the database!");
            else
                return StatusCode(500, "Error: Your coffee was not added to the database...");
        }

        //deletes a coffee from the database
        [HttpDelete]
        public IActionResult DeleteCoffee([FromBody]Coffee coffee)
        {
            if (DB.Delete(coffee))
                return StatusCode(200, "Your coffee was deleted from the database!");
            else
                return StatusCode(500, "Error: Your coffee was not deleted from the database...");
        }

        //updates a coffee on the database
        [HttpPut]
        public IActionResult UpdateCoffee([FromBody]Coffee coffee)
        {
            if (DB.Update(coffee))
                return StatusCode(200, "Your coffee was updated on the database!");
            else
                return StatusCode(500, "Error: Your coffee was not updated on the database...");
        }

    }
}
