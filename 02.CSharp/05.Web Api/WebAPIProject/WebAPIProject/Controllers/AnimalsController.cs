using ConsoleToWebAPIProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;

namespace ConsoleToWebAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        /// <summary>
        /// animals - list containing AnimalModel objects
        /// </summary>
        public List<AnimalModel> animals;

        public AnimalsController()
        {
            animals = new List<AnimalModel>()
            {
                new AnimalModel{ Id = 1, Name = "Cat" },
                new AnimalModel{ Id = 2, Name = "Dog"}
            };
        }

        /// <summary>
        /// GetAnimal - action method returns list of all the animals when "~/api/animals" endpoint is hit
        /// </summary>
        /// <returns> list of AnimalModel objects</returns>
        [Route("", Name = "All")]
        public IActionResult GetAnimals()
        {
            return Ok(animals);
        }

        /// <summary>
        /// GetAnimalsTest - action method is used to let client know on which location further processing of "~/api/animals/test" is carried out
        /// </summary>
        /// <returns> returns location information i.e on what action method or route the processing is carried out </returns>
        [Route("test")]
        public IActionResult GetAnimalsTest()
        {
            //return Accepted();    // just send status code as 202
            //return Accepted("~/api/animals");   // send 202 code with some data(proper action method) or (to send 202 status code and hardcoded accepted at location)
            //return AcceptedAtAction("GetAnimals");   // to send 202 status code but here u can get url from already created action method i.e nameOfActionMethod( no need to hardcode)
            return AcceptedAtRoute("All");  // send 202 and location using route name
        }


        /// <summary>
        /// GetAnimalsTest2 - action method is used to simulate BadRequest response when url route has cat as endpoint
        /// </summary>
        /// <param name="name"> name variable from url route is name of animal </param>
        /// <returns> badrequest when url route has cat else return an AnimalModel object </returns>
        [Route("{name}")]
        public IActionResult GetAnimalsTest2(string name)
        {
            if(name == "cat")
            {
                return BadRequest();
            }
            return Ok(new AnimalModel() { Id = 1, Name = name});
        }


        /// <summary>
        /// GetAnimals - action method which is executed for a POST call on "~/api/animals", appends the incomming animal objects to animals list and returns 201
        /// </summary>
        /// <param name="animal"> animal - is animal object which represents AnimalModel object in .NET type</param>
        /// <returns> status 201 and newly craeted AnimalModel object on creation of the object</returns>
        [Route("")]
        [HttpPost]
        public IActionResult GetAnimals(AnimalModel animal)
        {
            animals.Add(animal);
            return Created("~/api/animals/" + animal.Id, animal);
        }


        /// <summary>
        /// GetAnimalById - action method to search for AnimalModel object and if an error then handles it accordingly
        /// </summary>
        /// <param name="id"> id variable represent id of an AnimalModel object</param>
        /// <returns> BadRequest() when id = 0, when no such id exits returns NotFount() else returns the AnimalModel object</returns>
        [Route("{id:int}")]
        public IActionResult GetAnimalById(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            AnimalModel ?animal = animals.FirstOrDefault(x => x.Id == id);
            if(animal == null)
            {
                return NotFound();
            }
            return Ok(animal);
        }
    }
}
