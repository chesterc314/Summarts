using HtmlAgilityPack;
using SummArts.Helpers.Models;

namespace SummArts.Helpers
{
    public class ArticleProvider : IArticleProvider
    {
        private readonly HtmlWeb _web;

        public ArticleProvider()
        {
            _web = new HtmlWeb();
        }

        public Article FetchArticle(string url)
        {
            var doc = _web.Load(url);
            var titleElement = doc.DocumentNode.SelectSingleNode("//title");
            var articleElement = doc.DocumentNode.SelectSingleNode("//article") ?? doc.DocumentNode.SelectSingleNode("//main");
            var title = titleElement != null ? titleElement.InnerText: null;
            var fullText = articleElement != null ? articleElement.InnerText : null;
            return new Article
            {
                Title = title,
                FullText = fullText
            };
        }
    }
}