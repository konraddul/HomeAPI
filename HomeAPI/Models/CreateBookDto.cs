using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAPI.Models
{
    public class CreateBookDto
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public int CoverId { get; set; }
        public int NumberOfPages { get; set; }
        public int PublicationDate { get; set; }
        public int TypeId { get; set; }
        public string Comment { get; set; }
    }
}