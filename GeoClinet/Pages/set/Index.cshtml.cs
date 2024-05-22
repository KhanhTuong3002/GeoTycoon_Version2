using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Entites;
using DataAccess;

namespace GeoClinet.Pages.set
{
    public class IndexModel : PageModel
    {
        private readonly DataAccess.GeoTycoonDbcontext _context;

        public IndexModel(DataAccess.GeoTycoonDbcontext context)
        {
            _context = context;
        }

        public IList<SetQuestion> SetQuestion { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public async Task OnGetAsync()
        {
            var query = _context.SetQuestions.AsQueryable();

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                query = query.Where(s => s.SetName.Contains(SearchTerm));
            }

            SetQuestion = await query.ToListAsync();
        }
    }
}
