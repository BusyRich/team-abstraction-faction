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
    public class SampleDataController : Controller
    {

        private DBServices DB = new DBServices();

        //selects all the coffees from the DB
        [HttpGet("[action]")]
        public IEnumerable<Coffee> selectCoffee()
        {
            Coffee[] coffees = DB.Select();

            return Enumerable.Range(0, coffees.Count()).Select(index => new Coffee
            {
                ProductID = coffees[index].ProductID,
                CoffeeName = coffees[index].CoffeeName,
                Price = coffees[index].Price
            });
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
