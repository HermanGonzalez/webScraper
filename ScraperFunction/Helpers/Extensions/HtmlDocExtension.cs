using HtmlAgilityPack;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;
using ScraperFunction.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScraperFunction.Helpers.Extensions
{
    public static class HtmlDocExtension
    {
        public static IEnumerable<EventData> ParseHtmlDocumentToBooks(this HtmlDocument currentPage) {
            var booksNode = currentPage.DocumentNode.Descendants("article");

            foreach (var book in booksNode)
                yield return book.ParseHtmlNodeToEvent();
        }

        public static EventData ParseHtmlNodeToEvent(this HtmlNode node) {
            var book = new Book
            {
                Name = node.GetName(),
                Price = node.GetPrice(),
                Rating = node.GetRating()
            };

            var dataAsJson = JsonConvert.SerializeObject(book);
            

            return new EventData(Encoding.UTF8.GetBytes(dataAsJson));
        }

        public static string GetNextPage(this HtmlDocument currentPage) {
            var pager = currentPage.DocumentNode.Descendants("li").Where(node => node.GetAttributeValue("class", "").Contains("next")).FirstOrDefault();

            return pager != null ? pager.SelectSingleNode("a").GetAttributeValue("href", "") : null;
        }

        private static string GetName(this HtmlNode node) {
            return node.SelectSingleNode("h3/a").GetAttributeValue("title", "");
        }

        private static string GetPrice(this HtmlNode node) {
            return node.SelectSingleNode("div[@class='product_price']/p[@class='price_color']").InnerText;
        }

        private static int GetRating(this HtmlNode node) {
            string subString = "star-rating";
            string rateClass = node.SelectSingleNode($"p[contains(@class,'{subString}')]").GetAttributeValue("class", "");
            int indexOfSubString = rateClass.IndexOf(subString);
            
            rateClass = rateClass.Remove(indexOfSubString, subString.Length).Trim();

            switch (rateClass) {
                case "One":
                    return 1;
                case "Two":
                    return 2;
                case "Three":
                    return 3;
                case "Four":
                    return 4;
                case "Five":
                    return 5;
                default:
                    return 0;
            }
        }
    }
}
