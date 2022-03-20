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
    public interface ICoverService
    {
        CoverDto GetById(int id);
        PageResult<CoverDto> GetAll(BasicPageQuery query);
        int Create(CoverDto dto);
        void Delete(int id);
        void Update(int id, CoverDto dto);
    }
    public class CoverService : ICoverService
    {
        private readonly HomeDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<CoverService> _logger;

        public CoverService(HomeDbContext dbContext, IMapper mapper, ILogger<CoverService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public int Create(CoverDto dto)
        {
            var cover = _mapper.Map<Cover>(dto);
            _dbContext.Covers.Add(cover);
            _dbContext.SaveChanges();
            return cover.Id;
        }

        public void Delete(int id)
        {
            var cover = _dbContext
                .Covers
                .FirstOrDefault(c => c.Id == id);
            if (cover is null) throw new NotFoundException("Cover not found");
            _dbContext.Covers.Remove(cover);
            _dbContext.SaveChanges();
        }

        public PageResult<CoverDto> GetAll(BasicPageQuery query)
        {
            var baseQuery = _dbContext
                .Covers;

            var covers = baseQuery
                .Skip(query.PageSize * (query.Page - 1))
                .Take(query.PageSize)
                .ToList();

            var coversDtos = _mapper.Map<List<CoverDto>>(covers);
            var result = new PageResult<CoverDto>(coversDtos, baseQuery.Count(), query.PageSize, query.Page);
            return result;
        }

        public CoverDto GetById(int id)
        {
            var cover = _dbContext
                .Covers
                .FirstOrDefault(c => c.Id == id);

            if (cover is null) throw new NotFoundException("Cover not found");
            var result = _mapper.Map<CoverDto>(cover);
            return result;
        }

        public void Update(int id, CoverDto dto)
        {
            var cover = _dbContext
                 .Covers
                 .FirstOrDefault(c => c.Id == id);

            if (cover is null) throw new NotFoundException("Cover not found");

            cover.Name = dto.Name;

            _dbContext.SaveChanges();
        }
    }
}
