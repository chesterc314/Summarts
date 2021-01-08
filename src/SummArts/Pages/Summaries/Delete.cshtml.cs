using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SummArts.Models;
using SummArts.Persistence;

namespace SummArts.Pages.Summaries
{
    public class DeleteModel : PageModel
    {
        private readonly SummArtsContext _context;

        public DeleteModel(SummArtsContext context)
        {
            _context = context;
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Summary = await _context.Summary.FindAsync(id);

            if (Summary != null)
            {
                _context.Summary.Remove(Summary);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
