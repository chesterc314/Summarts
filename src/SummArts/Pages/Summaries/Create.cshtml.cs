using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public CreateModel(SummArtsContext context, IDateProvider dateProvider, ISummarizer summarizer, IArticleProvider articleProvider)
        {
            _context = context;
            _dateProvider = dateProvider;
            _summarizer = summarizer;
            _articleProvider = articleProvider;
        }

        public IActionResult OnGet()
        {
            return Page();
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
                        ModelState.AddModelError("", "Error retrieving article. To manually summarize an article, please enter the Raw Text.");
                        return Page();
                    }
                    var summary = _summarizer.Summarize(article.FullText);
                    Summary.SummaryText = summary;
                }
                else
                {
                    ModelState.AddModelError("", "Please enter a Source Url or Raw Text");
                    return Page();
                }
            }

            Summary.CreatedDate = Summary.UpdatedDate = _dateProvider.UtcNow;

            _context.Summary.Add(Summary);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
