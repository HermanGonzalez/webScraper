using DataServeFunction.Configuration;
using DataServeFunction.Ports.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataServeFunction.Ports.Database
{
    public class BooksRepository : BaseRepository<Book>
    {
        public BooksRepository(BooksStorageContext context) : base(context, "Books") { 
        }
    }
}
