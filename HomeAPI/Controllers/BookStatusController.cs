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
    [Route("api/bookstatus")]
    [ApiController]
    [Authorize]
    public class BookStatusController : ControllerBase
    {
        private readonly IBookStatusService _bookStatusService;
        public BookStatusController(IBookStatusService bookStatusService)
        {
            _bookStatusService = bookStatusService;
        }
        [HttpGet("{id}")]
        public ActionResult<BookStatusDto> Get([FromRoute] int id)
        {
            var bookStatus = _bookStatusService.GetById(id);
            return Ok(bookStatus);
        }
        [HttpGet("all")]
        public ActionResult<IEnumerable<BookStatusDto>> GelAll([FromQuery] BasicPageQuery query)
        {
            var bookStatusDtos = _bookStatusService.GetAll(query);
            return Ok(bookStatusDtos);
        }
        [HttpPost]
        [Authorize(Roles ="Administrator")]
        public ActionResult CreateAuthor([FromBody] BookStatusDto dto)
        {
            var id = _bookStatusService.Create(dto);
            return Created($"api/bookstatus/{id}", null);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public ActionResult Update([FromRoute] int id, [FromBody] BookStatusDto dto)
        {
            _bookStatusService.Update(id, dto);
            return Ok();
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete([FromRoute] int id)
        {
            _bookStatusService.Delete(id);
            return NoContent();
        }
    }
}
