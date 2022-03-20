using HomeAPI.Models;
using HomeAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HomeAPI.Controllers
{
    [Route("api/book")]
    [ApiController]
    [Authorize]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }
        [HttpPut("{id}")]
        public ActionResult Update([FromRoute]int id, [FromBody] UpdateBookDto dto)
        {
            _bookService.Update(id, dto);
            return Ok();
        }
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute]int id)
        {
            _bookService.Delete(id);
            return NoContent();
        }
        [HttpPost]
        public ActionResult CreateBook([FromBody]CreateBookDto dto)
        {
            var userId = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var id = _bookService.Create(dto);

            return Created($"/api/book/{id}", null);
        }
        [HttpGet("all")]
        public ActionResult<IEnumerable<BookDto>> GetAll([FromQuery] BookQuery query)
        {
            var bookDtos = _bookService.GetAll(query);
            return Ok(bookDtos);
        }
        [HttpGet("{id}")]
        public ActionResult<BookDto> Get([FromRoute] int id)
        {
            var book = _bookService.GetById(id);
            return Ok(book);
        }
    }
}
