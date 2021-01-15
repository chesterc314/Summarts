using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Persistence.Interface;
using SummArts.Models;

namespace SummArts.Pages.Summaries
{
    public class IndexModel : PageModel
    {
        private readonly IRepository<Summary, int> _repository;
        public IndexModel(IRepository<Summary, int> repository)
        {
            _repository = repository;
        }

        public IList<Summary> Summary { get; set; }

        public IActionResult OnGet()
        {
            Summary = _repository.GetAll().OrderByDescending(s => s.UpdatedDate).ToList();

            return Page();
        }
    }
}
