using System.Collections.Generic;
using System.Linq;
using BasicExample.Models;
using Microsoft.AspNetCore.Mvc;

namespace BasicExample.Controllers
{
    [Route("api/[controller]")]
    public class PeopleController : Controller
    {
        private readonly IEnumerable<PersonDto> people;
        public PeopleController()
        {
            people = new List<PersonDto>() {
                new PersonDto { Id = 1, Name = "Fanie", Email = "fanie@reynders.co" },
                new PersonDto { Id = 2, Name = "Maarten", Email = "maarten@example.com" },
                new PersonDto { Id = 3, Name = "Marcel", Email = "marcel@example.com" }
            };
        }

        private PersonDto GetPerson(int id)
        {
            return people.Single(p => p.Id == id);
        }
        [HttpGet(Name = "get-people")]
        public IActionResult Get()
        {
            return Ok(people);
        }

        [HttpGet("{id}", Name = "get-person")]
        public IActionResult Get(int id)
        {
            var person = GetPerson(id);
            return Ok(person);
        }

        [HttpPost(Name = "create-person")]
        public IActionResult Post([FromBody]PersonDto person)
        {
            person.Id = people.Count() + 1;
            ((List<PersonDto>)people).Add(person);
            return Ok();
        }

        [HttpPut("{id}", Name = "update-person")]
        public IActionResult Put(int id, [FromBody]PersonDto person)
        {
            var oldPerson = GetPerson(id);

            oldPerson.Name = person.Name;
            oldPerson.Email = person.Email;
            return NoContent();
        }
        
        [HttpDelete("{id}", Name = "delete-person")]
        public IActionResult Delete(int id)
        {
            var person = GetPerson(id);

            ((List<PersonDto>)people).Remove(person);
            return Ok();
        }
    }
}
