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
    [Route("api/country")]
    [ApiController]
    [Authorize]
    public class CountryController :ControllerBase
    {
        private readonly ICountryService _countryService;
        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }
        [HttpGet("{id}")]
        public ActionResult<CountryDto> Get([FromRoute]int id)
        {
            var country = _countryService.GetById(id);
            return Ok(country);
        }
        [HttpGet("all")]
        public ActionResult<IEnumerable<CountryDto>> GelAll([FromQuery] BasicPageQuery query)
        {
            var countriesDtos = _countryService.GetAll(query);
            return Ok(countriesDtos);
        }
        [HttpPost]
        [Authorize(Roles ="Administrator")]
        public ActionResult CreateCountry([FromBody] CountryDto dto)
        {
            var id = _countryService.Create(dto);
            return Created($"api/country/{id}", null);
        }
        [HttpPut("{id}")]
        [Authorize(Roles ="Administrator")]
        public ActionResult Update([FromRoute] int id,[FromBody] CountryDto dto)
        {
            _countryService.Update(id, dto);
            return Ok();
        }
        [HttpDelete("{id}")]
        [Authorize(Roles ="Administrator")]
        public ActionResult Delete([FromRoute] int id)
        {
            _countryService.Delete(id);
            return NoContent();
        }
    }
}
