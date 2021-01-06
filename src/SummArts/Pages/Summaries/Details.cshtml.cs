using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SummArts.Models;
using SummArts.Persistence;

namespace SummArts.Pages.Summaries
{
    public class DetailsModel : PageModel
    {
        private readonly SummArts.Persistence.SummArtsContext _context;

        public DetailsModel(SummArts.Persistence.SummArtsContext context)
        {
            _context = context;
        }

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
    }
}
