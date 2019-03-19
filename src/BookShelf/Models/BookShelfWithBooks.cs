using System;
using System.Collections.Generic;

namespace BookShelf.Models
{
    public class BookShelfDetails
    {
        public Guid Id { get; set; }
        public List<Book> Books { get; set; }
    }
}