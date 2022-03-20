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
    public interface IPersonService
    {
        PersonDto GetById(int id);
        PageResult<PersonDto> GetAll(BasicPageQuery query);
        int Create(PersonDto dto);
        void Delete(int id);
        void Update(int id, PersonDto dto);
    }
    public class PersonService : IPersonService
    {
        private readonly HomeDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<PersonService> _logger;

        public PersonService(HomeDbContext dbContext, IMapper mapper, ILogger<PersonService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public int Create(PersonDto dto)
        {
            var author = _mapper.Map<Author>(dto);
            _dbContext.Authors.Add(author);
            _dbContext.SaveChanges();
            return author.Id;
        }

        public void Delete(int id)
        {
            var person = _dbContext
                .Person
                .FirstOrDefault(c => c.Id == id);
            if (person is null) throw new NotFoundException("Person not found");
            _dbContext.Person.Remove(person);
            _dbContext.SaveChanges();
        }

        public PageResult<PersonDto> GetAll(BasicPageQuery query)
        {
            var baseQuery = _dbContext
                .Person;

            var person = baseQuery
                .Skip(query.PageSize * (query.Page - 1))
                .Take(query.PageSize)
                .ToList();

            var personDtos = _mapper.Map<List<PersonDto>>(person);
            var result = new PageResult<PersonDto>(personDtos, baseQuery.Count(), query.PageSize, query.Page);
            return result;
        }

        public PersonDto GetById(int id)
        {
            var person = _dbContext
                .Person
                .FirstOrDefault(c => c.Id == id);

            if (person is null) throw new NotFoundException("Person not found");
            var result = _mapper.Map<PersonDto>(person);
            return result;
        }

        public void Update(int id, PersonDto dto)
        {
            var person = _dbContext
                 .Person
                 .FirstOrDefault(c => c.Id == id);

            if (person is null) throw new NotFoundException("Person not found");

            person.FirstName = dto.FirstName;
            person.LastName = dto.LastName;
            person.PhoneNumber = dto.PhoneNumber;

            _dbContext.SaveChanges();
        }
    }
}
