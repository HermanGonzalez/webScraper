using System.Collections.Generic;

namespace ScraperFunction.Data
{
    public class Page
    {
        public  List<Book> Books { get; set; }
        public string nextPage { get; set; }
    }
}
