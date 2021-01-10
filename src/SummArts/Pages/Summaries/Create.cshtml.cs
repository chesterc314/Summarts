using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using SummArts.Helpers;
using SummArts.Models;
using SummArts.Persistence;

namespace SummArts.Pages.Summaries
{
    public class CreateModel : PageModel
    {
        private readonly SummArtsContext _context;
        private readonly IDateProvider _dateProvider;
        private readonly ISummarizer _summarizer;
        private readonly IArticleProvider _articleProvider;
        private readonly ISentimentAnalyzer _sentimentAnalyzer;
        private readonly IHttpClient _httpClient;

        private readonly IConfiguration _configuration;

        public CreateModel(SummArtsContext context,
        IDateProvider dateProvider,
        ISummarizer summarizer,
        IArticleProvider articleProvider,
        ISentimentAnalyzer sentimentAnalyzer,
        IHttpClient httpClient, 
        IConfiguration configuration)
        {
            _context = context;
            _dateProvider = dateProvider;
            _summarizer = summarizer;
            _articleProvider = articleProvider;
            _sentimentAnalyzer = sentimentAnalyzer;
            _httpClient = httpClient;
            _configuration = configuration;
        }

        private void BulkInsertArticlesFromNewsAPI()
        {
            var headers = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("Accept", "application/json") };
            var hostInfoResponse = _httpClient.Get("https://ipinfo.io/", headers);
            var hostInfoParent = JObject.Parse(hostInfoResponse.Content);
            var country = hostInfoParent.Value<string>("country").ToLower();
            var apiKey = _configuration["NewsAPIKey"];
            var newsApiResponse = _httpClient.Get($"https://newsapi.org/v2/top-headlines?country={country}&apiKey={apiKey}&pageSize=40", headers);
            var newsApiParent = JObject.Parse(newsApiResponse.Content);
            var articles = newsApiParent.Value<JArray>("articles");

            foreach (var article in articles)
            {
                var articleUrl = article.Value<string>("url");
                var articleTitle = article.Value<string>("title");
                Summary = new Summary();
                Summary.SourceUrl = articleUrl;
                Summary.Category = Category.News;
                var summary = _context.Summary.FirstOrDefault(m => m.Title == articleTitle);
                if (summary == null)
                {
                    var errors = AddSummaryAndSentiment();
                    Summary.Title = articleTitle;
                    if (!errors.Any())
                    {

                        Summary.CreatedDate = Summary.UpdatedDate = _dateProvider.UtcNow;
                        _context.Summary.Add(Summary);
                        _context.SaveChanges();
                    }
                }
            }
        }

        public IActionResult OnGet(bool isBulk)
        {
            if (isBulk)
            {
                BulkInsertArticlesFromNewsAPI();
                return RedirectToPage("./Index");
            }
            else
            {
                return Page();
            }

        }

        [BindProperty]
        public Summary Summary { get; set; }

        public static List<SelectListItem> ListOfCategories =>
        Enum.GetValues(typeof(Category))
        .Cast<Category>()
        .Select(c => new SelectListItem(c.ToString(), ((int)c).ToString())).Prepend(new SelectListItem("Select One", ""))
        .ToList();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (Summary.RawText != null)
            {
                if (Summary.SourceUrl != null)
                {
                    ModelState.AddModelError("", "If you entered Raw Text, then you can specify a Source Url, please clear or alternativing you can clear the Raw Text");
                    return Page();
                }
                var summary = _summarizer.Summarize(Summary.RawText);
                Summary.SummaryText = summary;
            }
            else
            {
                var errors = AddSummaryAndSentiment();
                if (errors.Any())
                {
                    foreach (var error in errors)
                    {
                        ModelState.AddModelError("", error);
                    }

                    return Page();
                }
            }

            Summary.CreatedDate = Summary.UpdatedDate = _dateProvider.UtcNow;

            _context.Summary.Add(Summary);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        public List<string> AddSummaryAndSentiment()
        {
            var errors = new List<string>();
            if (Summary.SourceUrl != null)
            {
                var article = _articleProvider.FetchArticle(Summary.SourceUrl);

                var title = article.Title;
                var fullText = article.FullText;
                if (title != null)
                {
                    Summary.Title = title;
                }
                if (fullText != null)
                {
                    Summary.RawText = fullText;
                }
                else
                {
                    errors.Add("Error retrieving article. This article cannot be added. Please contact tech support");
                }
                if (!errors.Any())
                {
                    var sentimentScore = _sentimentAnalyzer.DetermineSentimentScore(article.FullText);
                    var summary = _summarizer.Summarize(article.FullText);
                    Summary.SummaryText = summary;
                    Summary.Sentiment = Summary.DetermineSentiment(sentimentScore);
                }
            }
            else
            {
                errors.Add("Please enter a Source Url");
            }

            return errors;
        }
    }
}
