using AngleSharp;
using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        var url = "https://www.ukrinform.ua/tag-ekologia"; // Replace with your URL

        var config = Configuration.Default.WithDefaultLoader();
        var context = BrowsingContext.New(config);

        var document = await context.OpenAsync(url);

        var articles = document.QuerySelectorAll("article"); // Select all news articles

        foreach (var article in articles)
        {
            var figure = article.QuerySelector("figure");
            var links = figure.QuerySelector("a")?.GetAttribute("href");

            if (!string.IsNullOrWhiteSpace(links))
            {
                Console.WriteLine(links);
            }
        }
    }
}
