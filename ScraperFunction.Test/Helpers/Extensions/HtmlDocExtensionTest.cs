using Azure.Messaging.EventHubs;
using HtmlAgilityPack;
using ScraperFunction.Helpers.Extensions;
using ScraperFunction.Helpers.Parsers.Contexts;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace ScraperFunction.Test.Helpers.Extensions
{
    public class HtmlDocExtensionTest
    {
        public HtmlDocument GetPage(bool first = true)
        {
            var file = first ? File.ReadAllText(@"Resources/TestPage.html") : File.ReadAllText(@"Resources/LastPage.html");

            return (new HtmlParser()).Parse(file);
        }

        //passes
        [Fact]
        public void Should_parse_page_into_books_enumerator()
        {
            var page = GetPage();
            var books = new List<EventData>();

            foreach (var book in page.ParseHtmlDocumentToBooks())
                books.Add(book);

            Assert.Equal(20, books.Count);
        }

        [Fact]
        public void Should_find_and_return_next_page()
        {
            var page = GetPage();
            string nextPage = page.GetNextPage(); 

            Assert.Equal("catalogue/page-2.html", nextPage);
        }

        [Fact]
        public void Should_return_null_when_last_page()
        {
            var page = GetPage(false);
            string nextPage = page.GetNextPage();

            Assert.Null(nextPage);
        }
    }
}
