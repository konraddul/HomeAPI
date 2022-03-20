using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAPI.Models
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Cover { get; set; }
        public int NumberOfPages { get; set; }
        public int PublicationDate { get; set; }
        public string Type { get; set; }
        public string Comment { get; set; }
        public string BookStatus { get; set; }
        public int Grade { get; set; }
        public bool Favourite { get; set; }
        public bool Borrowed { get; set; }
    }
}
