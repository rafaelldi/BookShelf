using System.Collections.Generic;

namespace BookShelf.Models
{
    public class BookShelfDetails
    {
        public string Id { get; set; }
        public List<Book> Books { get; set; }
    }
}