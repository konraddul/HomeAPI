using HomeAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAPI
{
    public class BookSeeder
    {
        private readonly HomeDbContext _dbContext;

        public BookSeeder(HomeDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                var pendingMigrations = _dbContext.Database.GetPendingMigrations();
                if(pendingMigrations != null && pendingMigrations.Any())
                {
                    _dbContext.Database.Migrate();
                }
                if(!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }
                if(!_dbContext.Countries.Any())
                {
                    var countries = GetCountries();
                    _dbContext.Countries.AddRange(countries);
                    _dbContext.SaveChanges();
                }
                if(!_dbContext.Authors.Any())
                {
                    var authors = GetAuthors();
                    _dbContext.Authors.AddRange(authors);
                    _dbContext.SaveChanges();
                }
                if (!_dbContext.BookTypes.Any())
                {
                    var bookTypes = GetBookTypes();
                    _dbContext.BookTypes.AddRange(bookTypes);
                    _dbContext.SaveChanges();
                }
                if (!_dbContext.BookStatuses.Any())
                {
                    var bookStatuses = GetBookStatuses();
                    _dbContext.BookStatuses.AddRange(bookStatuses);
                    _dbContext.SaveChanges();
                }
                if (!_dbContext.Covers.Any())
                {
                    var covers = GetCovers();
                    _dbContext.Covers.AddRange(covers);
                    _dbContext.SaveChanges();
                }
                if (!_dbContext.Books.Any())
                {
                    var books = GetBooks();
                    _dbContext.Books.AddRange(books);
                    _dbContext.SaveChanges();
                }
            }
                
        }
        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "User"
                },
                new Role()
                {
                    Name = "Administrator"
                }
            };
            return roles;
        }
        private IEnumerable<Country> GetCountries()
        {
            var countries = new List<Country>()
            {
                new Country()
                {
                    Name = "Polska",
                    Shortcut = "PL"
                },
                new Country()
                {
                    Name = "Wielka brytania",
                    Shortcut = "GB"
                },
                new Country()
                {
                    Name = "Stany zjednoczone",
                    Shortcut = "US"
                },
                new Country()
                {
                    Name = "Rosja",
                    Shortcut = "RU"
                },
                new Country()
                {
                    Name = "Niemcy",
                    Shortcut = "DE"
                }
            };
            return countries;
        }
        private IEnumerable<Author> GetAuthors()
        {
            var authors = new List<Author>()
            {
                new Author()
                {
                    FirstName = "Stephen",
                    LastName = "King",
                    CountryId = 3,
                    DateOfBirth = DateTime.Parse("1947-09-21")
                },
                new Author()
                {
                    FirstName = "J.R.R.",
                    LastName = "Tolkien",
                    CountryId = 2,
                    DateOfBirth = DateTime.Parse("1982-01-03"),
                    DateOfDeath = DateTime.Parse("1973-09-02")
                },
                new Author()
                {
                    FirstName = "H.P.",
                    LastName = "Lovecraft",
                    CountryId = 3,
                    DateOfBirth = DateTime.Parse("1890-08-20"),
                    DateOfDeath = DateTime.Parse("1937-03-15")
                },
                new Author()
                {
                    FirstName = "George R.R.",
                    LastName = "Martin",
                    CountryId = 3,
                    DateOfBirth = DateTime.Parse("1948-09-20")
                }
            };
            return authors;
        }
        private IEnumerable<BookType> GetBookTypes()
        {
            var bookTypes = new List<BookType>()
            {
                new BookType()
                {
                    Name = "Literatura obyczajowa, romans"
                },
                new BookType()
                {
                    Name = "Kryminał, sensacja, thriller"
                },
                new BookType()
                {
                    Name = "Fantasy, science fiction"
                },
                new BookType()
                {
                    Name = "Reportaż"
                },
                new BookType()
                {
                    Name = "Horror"
                },
                new BookType()
                {
                    Name = "Literatura młodzieżowa"
                }
            };
            return bookTypes;
        }
        private IEnumerable<BookStatus> GetBookStatuses()
        {
            var bookStatuses = new List<BookStatus>()
            {
                new BookStatus()
                {
                    Name = "Nieprzeczytana"
                },
                new BookStatus()
                {
                    Name = "W trakcie czytania"
                },
                new BookStatus()
                {
                    Name = "Przeczytana"
                }
            };
            return bookStatuses;
        }
        private IEnumerable<Cover> GetCovers()
        {
            var covers = new List<Cover>()
            {
                new Cover()
                {
                    Name = "Miękka"
                },
                new Cover()
                {
                    Name = "Twarda"
                }
            };
            return covers;
        }
        private IEnumerable<Book> GetBooks()
        {
            var books = new List<Book>()
            {
                new Book()
                {
                    Title = "Bastion",
                    AuthorId = 1,
                    CoverId = 2,
                    NumberOfPages = 1165,
                    PublicationDate = 2017,
                    TypeId = 6,
                    Comment = "Konradek zaczął czytać i nie skończył :)",
                    BookStatusId = 1,
                },
                new Book()
                {
                    Title = "Hobbit",
                    AuthorId = 2,
                    CoverId = 2,
                    NumberOfPages = 300,
                    PublicationDate = 2017,
                    TypeId = 6,
                    Comment = "Konradek zaczął czytać i nie skończył :)",
                    BookStatusId = 1
                },
                new Book()
                {
                    Title = "Zew Cthulhu",
                    AuthorId = 3,
                    CoverId = 2,
                    NumberOfPages = 301,
                    PublicationDate = 2019,
                    TypeId = 8,
                    Comment = "Konradek zaczął czytać i nie skończył :)",
                    BookStatusId = 1
                }
            };
            return books;
        }
    }
}
