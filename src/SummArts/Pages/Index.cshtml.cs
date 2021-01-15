using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Persistence.Interface;
using SummArts.Models;
using SummArts.Persistence;

namespace SummArts.Pages
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
            Summary = _repository.GetAll();

            return Page();
        }
    }
}
