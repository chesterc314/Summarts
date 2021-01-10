using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.WindowsRuntime;

namespace SummArts.Models
{
    public class Summary : Entity<int>
    {
        public string Title { get; set; }
        [DisplayName("Summary")]
        public string SummaryText { get; set; }
        [DisplayName("Raw Text")]
        public string RawText { get; set; }
        [DisplayName("Source Url")]
        [Url]
        public string SourceUrl { get; set; }
        public Category Category { get; set; }
        public Sentiment Sentiment { get; set; }

        public Sentiment DetermineSentiment(double score)
        {
            if (score >= 0.05)
            {
                return Sentiment.Positive;
            }
            else if (score > -0.05 && score < 0.05)
            {
                return Sentiment.Neutral;
            }
            return Sentiment.Negative;
        }
    }
}