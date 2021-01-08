using System.Linq;
using OpenTextSummarizer;
using OpenTextSummarizer.ContentProviders;

namespace SummArts.Helpers
{
    public class Summarizer : ISummarizer
    {
        public string Summarize(string fullText)
        {
            var summarizedDocument = OpenTextSummarizer.Summarizer.Summarize(
                new DirectTextContentProvider(fullText),
                new SummarizerArguments()
                {
                    Language = "en",
                    MaxSummarySentences = 5
                });

            return summarizedDocument.Sentences.Aggregate("", (a, b) => $"{a} {b}");
        }
    }
}