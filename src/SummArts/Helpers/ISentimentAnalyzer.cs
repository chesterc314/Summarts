using SummArts.Models;

namespace SummArts.Helpers
{
    public interface ISentimentAnalyzer
    {
        double DetermineScoreSentiment(string text);
    }
}