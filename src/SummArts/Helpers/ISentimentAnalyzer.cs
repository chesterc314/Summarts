using SummArts.Models;

namespace SummArts.Helpers
{
    public interface ISentimentAnalyzer
    {
        double DetermineSentimentScore(string text);
    }
}