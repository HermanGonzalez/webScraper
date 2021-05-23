using HtmlAgilityPack;

namespace ScraperFunction.Helpers.Parsers
{
    public interface IHtmlParser
    {
        HtmlDocument Parse(string html);
    }
}
