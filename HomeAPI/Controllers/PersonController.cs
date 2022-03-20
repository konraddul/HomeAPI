using HomeAPI.Models;
using HomeAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAPI.Controllers
{
    [Route("api/person")]
    [ApiController]
    [Authorize]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }
        [HttpGet("{id}")]
        public ActionResult<PersonDto> Get([FromRoute] int id)
        {
            var person = _personService.GetById(id);
            return Ok(person);
        }
        [HttpGet("all")]
        public ActionResult<IEnumerable<PersonDto>> GelAll([FromQuery] BasicPageQuery query)
        {
            var personDtos = _personService.GetAll(query);
            return Ok(personDtos);
        }
        [HttpPost]
        public ActionResult CreatePerson([FromBody] PersonDto dto)
        {
            var id = _personService.Create(dto);
            return Created($"api/person/{id}", null);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public ActionResult Update([FromRoute] int id, [FromBody] PersonDto dto)
        {
            _personService.Update(id, dto);
            return Ok();
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete([FromRoute] int id)
        {
            _personService.Delete(id);
            return NoContent();
        }
    }
}
