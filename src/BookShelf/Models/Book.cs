using System;
using System.ComponentModel.DataAnnotations;

namespace BookShelf.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        public string Comment { get; set; }
    }
}