using System.Globalization;
using System.Threading;
using VaderSharp;

namespace SummArts.Helpers
{
    public class SentimentAnalyzer : ISentimentAnalyzer
    {
        private readonly SentimentIntensityAnalyzer _analyzer;
        public SentimentAnalyzer()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
            _analyzer = new SentimentIntensityAnalyzer();
        }
        public double DetermineSentimentScore(string text)
        {
            var results = _analyzer.PolarityScores(text);
            return results.Compound;
        }
    }
}