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
    [Route("api/cover")]
    [ApiController]
    [Authorize]
    public class CoverController : ControllerBase
    {
        private readonly ICoverService _coverService;
        public CoverController(ICoverService coverService)
        {
            _coverService = coverService;
        }
        [HttpGet("{id}")]
        public ActionResult<CoverDto> Get([FromRoute] int id)
        {
            var cover = _coverService.GetById(id);
            return Ok(cover);
        }
        [HttpGet("all")]
        public ActionResult<IEnumerable<CoverDto>> GelAll([FromQuery] BasicPageQuery query)
        {
            var coversDtos = _coverService.GetAll(query);
            return Ok(coversDtos);
        }
        [HttpPost]
        [Authorize(Roles ="Administrator")]
        public ActionResult CreateCover([FromBody] CoverDto dto)
        {
            var id = _coverService.Create(dto);
            return Created($"api/cover/{id}", null);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public ActionResult Update([FromRoute] int id, [FromBody] CoverDto dto)
        {
            _coverService.Update(id, dto);
            return Ok();
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete([FromRoute] int id)
        {
            _coverService.Delete(id);
            return NoContent();
        }
    }
}
