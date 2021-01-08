using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SummArts.Helpers;
using SummArts.Models;
using SummArts.Persistence;

namespace SummArts.Pages.Summaries
{
    public class EditModel : PageModel
    {
        private readonly SummArtsContext _context;
        private readonly IDateProvider _dateProvider;
        private readonly ISummarizer _summarizer;
        private readonly IArticleProvider _articleProvider;

        public EditModel(SummArtsContext context, IDateProvider dateProvider, ISummarizer summarizer, IArticleProvider articleProvider)
        {
            _context = context;
            _dateProvider = dateProvider;
            _summarizer = summarizer;
            _articleProvider = articleProvider;
        }

        [BindProperty]
        public Summary Summary { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Summary = await _context.Summary.FirstOrDefaultAsync(m => m.Id == id);

            if (Summary == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
   
            Summary.UpdatedDate = _dateProvider.UtcNow;
            _context.Attach(Summary).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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
            return _context.Summary.Any(e => e.Id == id);
        }
    }
}
