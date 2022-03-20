using AutoMapper;
using HomeAPI.Entities;
using HomeAPI.Exceptions;
using HomeAPI.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAPI.Services
{
    public interface ICountryService
    {
        int Create(CountryDto dto);
        void Update(int id, CountryDto dto);
        void Delete(int id);
        CountryDto GetById(int id);
        PageResult<CountryDto> GetAll(BasicPageQuery query);

    }
    public class CountryService : ICountryService
    {
        private readonly HomeDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<CountryService> _logger;

        public CountryService(HomeDbContext dbContext, IMapper mapper, ILogger<CountryService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }
        public int Create(CountryDto dto)
        {
            var country = _mapper.Map<Country>(dto);
            _dbContext.Countries.Add(country);
            _dbContext.SaveChanges();
            return country.Id;
        }

        public void Delete(int id)
        {
            var country = _dbContext
                .Countries
                .FirstOrDefault(c => c.Id == id);
            if (country is null) throw new NotFoundException("Country not found");
            _dbContext.Countries.Remove(country);
            _dbContext.SaveChanges();
        }

        public CountryDto GetById(int id)
        {
            var country = _dbContext
                .Countries
                .FirstOrDefault(c => c.Id == id);

            if (country is null) throw new NotFoundException("Country not found");
            var result = _mapper.Map<CountryDto>(country);
            return result;
        }

        public PageResult<CountryDto> GetAll(BasicPageQuery query)
        {
            var baseQuery = _dbContext
                .Countries;

            var countries = baseQuery
                .Skip(query.PageSize * (query.Page - 1))
                .Take(query.PageSize)
                .ToList();

            var countryDtos = _mapper.Map<List<CountryDto>>(countries);
            var result = new PageResult<CountryDto>(countryDtos, baseQuery.Count(), query.PageSize, query.Page);
            return result;
        }
            

        public void Update(int id, CountryDto dto)
        {
            var country = _dbContext
                 .Countries
                 .FirstOrDefault(c => c.Id == id);

            if(country is null) throw new NotFoundException("Country not found");

            country.Name = dto.Name;
            country.Shortcut = dto.Shortcut;

            _dbContext.SaveChanges();
        }
    }
}
