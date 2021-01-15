using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Persistence.Interface;
using SummArts.Models;

namespace SummArts.Pages.Summaries
{
    public class DetailsModel : PageModel
    {
        private readonly IRepository<Summary, int> _repository;
        public DetailsModel(IRepository<Summary, int> repository)
        {
            _repository = repository;
        }

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
    }
}
