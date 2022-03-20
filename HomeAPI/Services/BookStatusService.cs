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
    public interface IBookStatusService
    {
        BookStatusDto GetById(int id);
        PageResult<BookStatusDto> GetAll(BasicPageQuery query);
        int Create(BookStatusDto dto);
        void Delete(int id);
        void Update(int id, BookStatusDto dto);
    }
    public class BookStatusService : IBookStatusService
    {
        private readonly HomeDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<BookStatusService> _logger;

        public BookStatusService(HomeDbContext dbContext, IMapper mapper, ILogger<BookStatusService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public int Create(BookStatusDto dto)
        {
            var bookStatus = _mapper.Map<BookStatus>(dto);
            _dbContext.BookStatuses.Add(bookStatus);
            _dbContext.SaveChanges();
            return bookStatus.Id;
        }

        public void Delete(int id)
        {
            var bookStatus = _dbContext
                .BookStatuses
                .FirstOrDefault(c => c.Id == id);
            if (bookStatus is null) throw new NotFoundException("Book Status not found");
            _dbContext.BookStatuses.Remove(bookStatus);
            _dbContext.SaveChanges();
        }

        public PageResult<BookStatusDto> GetAll(BasicPageQuery query)
        {
            var baseQuery = _dbContext
                .BookStatuses;

            var bookStatuses = baseQuery
                .Skip(query.PageSize * (query.Page - 1))
                .Take(query.PageSize)
                .ToList();

            var bookStatusesDtos = _mapper.Map<List<BookStatusDto>>(bookStatuses);
            var result = new PageResult<BookStatusDto>(bookStatusesDtos, baseQuery.Count(), query.PageSize, query.Page);
            return result;
        }

        public BookStatusDto GetById(int id)
        {
            var bookStatus = _dbContext
                .BookStatuses
                .FirstOrDefault(c => c.Id == id);

            if (bookStatus is null) throw new NotFoundException("Book Status not found");
            var result = _mapper.Map<BookStatusDto>(bookStatus);
            return result;
        }
        public void Update(int id, BookStatusDto dto)
        {
            var bookStatus = _dbContext
                 .BookStatuses
                 .FirstOrDefault(c => c.Id == id);

            if (bookStatus is null) throw new NotFoundException("Book Status not found");

            bookStatus.Name = dto.Name;

            _dbContext.SaveChanges();
        }
    

        
    }
}
