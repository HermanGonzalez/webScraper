using Microsoft.Azure.Cosmos.Table;
using System;

namespace DataServeFunction.Ports.Dto
{
    public class Book : TableEntity
    {
        public Guid id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public int Rating { get; set; }

        public DateTime scrapedTime { get; set; }
    }
}
