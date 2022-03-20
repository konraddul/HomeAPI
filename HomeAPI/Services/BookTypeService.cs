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
    public interface IBookTypeService
    {
        int Create(BookTypeDto dto);
        void Update(int id, BookTypeDto dto);
        void Delete(int id);
        BookTypeDto GetById(int id);
        PageResult<BookTypeDto> GetAll(BasicPageQuery query);
    }
    public class BookTypeService : IBookTypeService
    {
        private readonly HomeDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<BookTypeService> _logger;
        public BookTypeService(HomeDbContext dbContext, IMapper mapper, ILogger<BookTypeService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public int Create(BookTypeDto dto)
        {
            var bookType = _mapper.Map<BookType>(dto);
            _dbContext.BookTypes.Add(bookType);
            _dbContext.SaveChanges();
            return bookType.Id;
        }

        public void Delete(int id)
        {
            var bookType = _dbContext
                .BookTypes
                .FirstOrDefault(b => b.Id == id);

            if (bookType is null) throw new NotFoundException("Book type not found");
            _dbContext.Remove(bookType);
            _dbContext.SaveChanges();
        }

        public PageResult<BookTypeDto> GetAll(BasicPageQuery query)
        {
            var baseQuery = _dbContext
                .BookTypes;

            var bookTypes = baseQuery
                .Skip(query.PageSize * (query.Page - 1))
                .Take(query.PageSize)
                .ToList();

            var bookTypesDtos = _mapper.Map<List<BookTypeDto>>(bookTypes);
            var result = new PageResult<BookTypeDto>(bookTypesDtos, baseQuery.Count(), query.PageSize, query.Page);
            return result;
        }

        public BookTypeDto GetById(int id)
        {
            var bookType = _dbContext
                .BookTypes
                .FirstOrDefault(b => b.Id == id);
            if (bookType is null) throw new NotFoundException("Book type not found");
            var result = _mapper.Map<BookTypeDto>(bookType);
            return result;
        }

        public void Update(int id, BookTypeDto dto)
        {
            var bookType = _dbContext
                .BookTypes
                .FirstOrDefault(b => b.Id == id);
            if (bookType is null) throw new NotFoundException("Book type not found");
            bookType.Name = dto.Name;
            _dbContext.SaveChanges();
        }
    }
}
