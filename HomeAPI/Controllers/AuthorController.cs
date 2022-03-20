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
    [Route("api/author")]
    [ApiController]
    [Authorize]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }
        [HttpGet("{id}")]
        public ActionResult<AuthorDto> Get([FromRoute] int id)
        {
            var author = _authorService.GetById(id);
            return Ok(author);
        }
        [HttpGet("all")]
        public ActionResult<IEnumerable<AuthorDto>> GelAll([FromQuery] BasicPageQuery query)
        {
            var authorsDtos = _authorService.GetAll(query);
            return Ok(authorsDtos);
        }
        [HttpPost]
        public ActionResult CreateAuthor([FromBody] UpdateAuthorDto dto)
        {
            var id = _authorService.Create(dto);
            return Created($"api/author/{id}", null);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public ActionResult Update([FromRoute] int id, [FromBody] UpdateAuthorDto dto)
        {
            _authorService.Update(id, dto);
            return Ok();
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete([FromRoute] int id)
        {
            _authorService.Delete(id);
            return NoContent();
        }
    }
}
