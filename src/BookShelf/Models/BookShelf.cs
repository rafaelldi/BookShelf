using System;
using System.ComponentModel.DataAnnotations;

namespace BookShelf.Models
{
    public class BookShelf
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}