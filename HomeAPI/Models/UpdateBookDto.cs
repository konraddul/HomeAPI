using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAPI.Models
{
    public class UpdateBookDto
    {
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public int CoverId { get; set; }
        public int NumberOfPages { get; set; }
        public int PublicationDate { get; set; }
        public int TypeId { get; set; }
        public string Comment { get; set; }
        public int BookStatusId { get; set; }
        public int Grade { get; set; }
        public bool Favourite { get; set; }
        public int BorrowId { get; set; }
    }
}
