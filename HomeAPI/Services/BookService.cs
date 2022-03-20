using AutoMapper;

using HomeAPI.Entities;
using HomeAPI.Exceptions;
using HomeAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HomeAPI.Services
{
    public interface IBookService
    {
        BookDto GetById(int id);
        PageResult<BookDto> GetAll(BookQuery query);
        int Create(CreateBookDto dto);
        void Delete(int id);
        void Update(int id, UpdateBookDto dto);
    }
    public class BookService : IBookService
    {
        private readonly HomeDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<BookService> _logger;
        private readonly IUserContextService _userContextService;
        public BookService(HomeDbContext dbContext, IMapper mapper, ILogger<BookService> logger, IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _userContextService = userContextService;
        }

        public int Create(CreateBookDto dto)
        {
            var book = _mapper.Map<Book>(dto);
            book.UserId = _userContextService.GetUserId;
            book.BookStatusId = 3;
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
            return book.Id;
        }

        public void Delete(int id)
        {
            _logger.LogWarning($"Book with id: {id} DELETE action invoked");

            var book = _dbContext
                .Books
                .FirstOrDefault(b => b.Id == id);
            if (book is null) throw new NotFoundException("Book not found");
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
        }

        public PageResult<BookDto> GetAll(BookQuery query)
        {
            var baseQuery = _dbContext
                .Books
                .Include(b => b.Author)
                .Include(b => b.BookStatus)
                .Include(b => b.Borrow)
                .Include(b => b.Cover)
                .Include(b => b.Type)
                .Include(b => b.User)
                .Where(b => query.SearchPhrase == null || (b.Title.ToLower().Contains(query.SearchPhrase.ToLower())
                                                            || (b.Author.FirstName + b.Author.LastName).ToLower().Contains(query.SearchPhrase.ToLower())));


            if(!_userContextService.User.IsInRole("Administrator"))
            {
                baseQuery = baseQuery.Where(b => b.UserId == _userContextService.GetUserId);
            }
            if(!string.IsNullOrEmpty(query.SortBy))
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Book, object>>>
                {
                    {nameof(Book.Title), b => b.Title },
                    {nameof(Book.Author), b => b.Author.LastName },
                    {nameof(Book.Type), b => b.Type.Name }
                };
                var selectedColumn = columnsSelector[query.SortBy];
                baseQuery = query.SortDirection == SortDirection.ASC ?
                    baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn); 
            }

            var books = baseQuery
                .Skip(query.PageSize * (query.Page - 1))
                .Take(query.PageSize)
                .ToList();

            var booksDtos = _mapper.Map<List<BookDto>>(books);
            var result = new PageResult<BookDto>(booksDtos, baseQuery.Count(), query.PageSize, query.Page);
            return result;
        }

        public BookDto GetById(int id)
        {
            var book = _dbContext
                .Books
                .Include(b => b.Author)
                .Include(b => b.BookStatus)
                .Include(b => b.Borrow)
                .Include(b => b.Cover)
                .Include(b => b.Type)
                .Include(b => b.User)
                .FirstOrDefault(b => b.Id == id);
            if (book is null) throw new NotFoundException("Book not found");

            var result = _mapper.Map<BookDto>(book);
            return result;
        }

        public void Update(int id, UpdateBookDto dto)
        {
            var book = _dbContext
                .Books
                .FirstOrDefault(r => r.Id == id);
            if (book is null) throw new NotFoundException("Book not found");
            book.Title = dto.Title;
            book.AuthorId = dto.AuthorId;
            book.CoverId = dto.CoverId;
            book.NumberOfPages = dto.NumberOfPages;
            book.PublicationDate = dto.PublicationDate;
            book.TypeId = dto.TypeId;
            book.Comment = dto.Comment;
            book.BookStatusId = dto.BookStatusId;
            book.Grade = dto.Grade;
            book.Favourite = dto.Favourite;
            book.BorrowId = dto.BorrowId;

            _dbContext.SaveChanges();
        }
    }
}
