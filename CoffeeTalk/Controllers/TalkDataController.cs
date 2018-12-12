using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeTalk.Services; //Imports DB Service
using CoffeeTalk.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeTalk.Controllers
{

    [Route("api/[controller]")]
    public class TalkDataController : Controller
    {

        private TalkDBServices DB = new TalkDBServices();

        //selects all the coffees from the DB
        [HttpGet("[action]")]
        public IEnumerable<Talk> selectCoffee()
        {
            Talk[] coffees = DB.Select();

            return Enumerable.Range(0, coffees.Count()).Select(index => new Talk
            {
                ProductID = coffees[index].ProductID,
                CoffeeName = coffees[index].CoffeeName,
                PicUrl = coffees[index].PicUrl,
                Description = coffees[index].Description,
            });
        }

        //creates a new coffee on the database
        [HttpPost("[action]")]
        public IActionResult CreateCoffee([FromBody]Talk coffee)
        {
            try
            {
                DB.Insert(coffee);
                return StatusCode(200, "Your coffee was added to the database!");
            }
            catch
            {
                return StatusCode(500, "Error: Your coffee was not added to the database...");
            }
        }
    }
}
