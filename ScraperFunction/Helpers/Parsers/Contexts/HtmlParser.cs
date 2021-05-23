using HtmlAgilityPack;
using System;

namespace ScraperFunction.Helpers.Parsers.Contexts
{
    public class HtmlParser : IHtmlParser
    {
        public HtmlDocument Parse(string html)
        {
            if (string.IsNullOrEmpty(html))
                throw new ArgumentNullException("HTMLP0001 empty page");

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            return htmlDoc;
        }
    }
}
