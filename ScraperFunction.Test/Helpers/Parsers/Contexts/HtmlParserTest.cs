using HtmlAgilityPack;
using ScraperFunction.Helpers.Parsers.Contexts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Xunit;

namespace ScraperFunction.Test.Helpers.Parsers.Contexts
{
    public class HtmlParserTest
    {
        private HtmlParser InitParser()
        {
            return new HtmlParser();
        }

        public string GetTestPage() {
           return File.ReadAllText(@"Resources/TestPage.html");
        }

        //passes
        [Fact]
        public void Should_get_html_document_object()
        {
            var context = InitParser();

            var result = context.Parse(GetTestPage());

            Assert.NotNull(result);
            Assert.IsType<HtmlDocument>(result);
        }

        //passes
        [Fact]
        public void Should_throw_exception_when_html_is_null()
        {
            var context = InitParser();

            Assert.Throws<ArgumentNullException>(() => context.Parse(null));
        }

    }
}
