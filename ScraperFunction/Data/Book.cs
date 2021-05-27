﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ScraperFunction.Data
{
    public class Book
    {
        public Guid id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public int Rating { get; set; }

        public DateTime scrapedTime { get; set; }
    }
}
