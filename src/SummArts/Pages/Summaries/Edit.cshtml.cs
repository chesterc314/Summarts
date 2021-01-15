using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Persistence.Interface;
using SummArts.Helpers;
using SummArts.Models;
using SummArts.Persistence;

namespace SummArts.Pages.Summaries
{
    public class EditModel : PageModel
    {
        private readonly IRepository<Summary, int> _repository;
        private readonly IDateProvider _dateProvider;
        private readonly ISummarizer _summarizer;
        private readonly IArticleProvider _articleProvider;

        public EditModel(IDateProvider dateProvider, ISummarizer summarizer, IArticleProvider articleProvider, IRepository<Summary, int> repository)
        {
            _dateProvider = dateProvider;
            _summarizer = summarizer;
            _articleProvider = articleProvider;
            _repository = repository;
        }

        [BindProperty]
        public Summary Summary { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Summary = _repository.Get(id.Value);

            if (Summary == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
   
            Summary.UpdatedDate = _dateProvider.UtcNow;

            try
            {
                _repository.Update(Summary);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SummaryExists(Summary.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool SummaryExists(int id)
        {
            return _repository.Count(x => x.Id == id) > 0;
        }
    }
}
