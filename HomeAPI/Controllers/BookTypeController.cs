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
    [Route("api/booktype")]
    [ApiController]
    [Authorize]
    public class BookTypeController : ControllerBase
    {
        private readonly IBookTypeService _bookTypeService;
        public BookTypeController(IBookTypeService bookTypeService)
        {
            _bookTypeService = bookTypeService;
        }
        [HttpGet("{id}")]
        public ActionResult<BookTypeDto> Get([FromRoute] int id)
        {
            var bookType = _bookTypeService.GetById(id);
            return Ok(bookType);
        }
        [HttpGet("all")]
        [Authorize(Roles = "Administrator")]
        public ActionResult<IEnumerable<BookTypeDto>> GelAll([FromQuery] BasicPageQuery query)
        {
            var bookTypesDtos = _bookTypeService.GetAll(query);
            return Ok(bookTypesDtos);
        }
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult CreateCountry([FromBody] BookTypeDto dto)
        {
            var id = _bookTypeService.Create(dto);
            return Created($"api/country/{id}", null);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public ActionResult Update([FromRoute] int id, [FromBody] BookTypeDto dto)
        {
            _bookTypeService.Update(id, dto);
            return Ok();
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete([FromRoute] int id)
        {
            _bookTypeService.Delete(id);
            return NoContent();
        }
    }
}
