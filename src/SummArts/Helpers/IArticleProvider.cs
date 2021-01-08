using SummArts.Helpers.Models;

namespace SummArts.Helpers 
{
    public interface IArticleProvider
    {
        Article FetchArticle(string url);
    }
}