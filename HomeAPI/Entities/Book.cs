using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAPI.Entities
{
    public class Book
    {
        public int Id { get; set;}
        public string Title { get; set; }
        public int? AuthorId { get; set; }
        public virtual Author Author { get; set; }
        public int? CoverId { get; set; }
        public virtual Cover Cover { get; set; }
        public int? NumberOfPages { get; set; }
        public int? PublicationDate { get; set; }
        public int? TypeId { get; set; }
        public virtual BookType Type { get; set; }
        public string? Comment { get; set; }
        public int? BookStatusId { get; set; }
        public virtual BookStatus BookStatus { get; set; }
        public int? Grade { get; set; }
        public bool Favourite { get; set; }
        public int? BorrowId { get; set; }
        public virtual Borrow Borrow { get; set; }
        public int? UserId { get; set; }
        public virtual User User { get; set; }
    }
}
