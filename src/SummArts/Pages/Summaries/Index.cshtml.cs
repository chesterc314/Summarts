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
    public class IndexModel : PageModel
    {
        private readonly SummArtsContext _context;

        public IndexModel(SummArtsContext context)
        {
            _context = context;
        }

        public IList<Summary> Summary { get;set; }

        public async Task OnGetAsync()
        {
            Summary = await _context.Summary.OrderByDescending(s => s.UpdatedDate).ToListAsync();
        }
    }
}
