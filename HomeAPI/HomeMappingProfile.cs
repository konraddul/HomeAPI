using AutoMapper;
using HomeAPI.Entities;
using HomeAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAPI
{
    public class HomeMappingProfile : Profile
    {
        public HomeMappingProfile()
        {
            CreateMap<Book, BookDto>()
                .ForMember(m => m.Author, c => c.MapFrom(s => s.Author.FirstName + " " + s.Author.LastName))
                .ForMember(m => m.Cover, c => c.MapFrom(s => s.Cover.Name))
                .ForMember(m => m.Type, c => c.MapFrom(s => s.Type.Name))
                .ForMember(m => m.BookStatus, c => c.MapFrom(s => s.BookStatus.Name))
                .ForMember(m => m.Borrowed, c => c.MapFrom(s => s.Borrow != null));
            CreateMap<CreateBookDto, Book>();

            CreateMap<CountryDto, Country>();
            CreateMap<Country, CountryDto>();

            CreateMap<BookTypeDto, BookType>();
            CreateMap<BookType, BookTypeDto>();

            CreateMap<UpdateAuthorDto, Author>();
            CreateMap<Author, UpdateAuthorDto>();
            CreateMap<Author, AuthorDto>()
                .ForMember(m => m.Country, c => c.MapFrom(s => s.Country.Name));

            CreateMap<CoverDto, Cover>();
            CreateMap<Cover, CoverDto>();

            CreateMap<BookStatusDto, BookStatus>();
            CreateMap<BookStatus, BookStatusDto>();
        }
        
    }
}
