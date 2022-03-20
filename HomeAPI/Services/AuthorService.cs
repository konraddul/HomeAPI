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
    public interface IAuthorService
    {
        AuthorDto GetById(int id);
        PageResult<AuthorDto> GetAll(BasicPageQuery query);
        int Create(UpdateAuthorDto dto);
        void Delete(int id);
        void Update(int id, UpdateAuthorDto dto);
    }
    public class AuthorService : IAuthorService
    {
        private readonly HomeDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthorService> _logger;

        public AuthorService(HomeDbContext dbContext, IMapper mapper, ILogger<AuthorService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public int Create(UpdateAuthorDto dto)
        {
            var author = _mapper.Map<Author>(dto);
            _dbContext.Authors.Add(author);
            _dbContext.SaveChanges();
            return author.Id;
        }

        public void Delete(int id)
        {
            var author = _dbContext
                .Authors
                .FirstOrDefault(c => c.Id == id);
            if (author is null) throw new NotFoundException("Author not found");
            _dbContext.Authors.Remove(author);
            _dbContext.SaveChanges();
        }

        public PageResult<AuthorDto> GetAll(BasicPageQuery query)
        {
            var baseQuery = _dbContext
                .Authors;

            var authors = baseQuery
                .Skip(query.PageSize * (query.Page - 1))
                .Take(query.PageSize)
                .ToList();

            var authorsDtos = _mapper.Map<List<AuthorDto>>(authors);
            var result = new PageResult<AuthorDto>(authorsDtos, baseQuery.Count(), query.PageSize, query.Page);
            return result;
        }

        public AuthorDto GetById(int id)
        {
            var author = _dbContext
                .Authors
                .FirstOrDefault(c => c.Id == id);

            if (author is null) throw new NotFoundException("Author not found");
            var result = _mapper.Map<AuthorDto>(author);
            return result;
        }

        public void Update(int id, UpdateAuthorDto dto)
        {
            var author = _dbContext
                 .Authors
                 .FirstOrDefault(c => c.Id == id);

            if (author is null) throw new NotFoundException("Author not found");

            author.FirstName = dto.FirstName;
            author.LastName = dto.LastName;
            author.CountryId = dto.CountryId;
            author.DateOfBirth = dto.DateOfBirth;
            author.DateOfDeath = dto.DateOfDeath;

            _dbContext.SaveChanges();
        }
    }
}
