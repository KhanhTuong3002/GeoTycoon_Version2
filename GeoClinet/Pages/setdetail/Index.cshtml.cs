using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Entites;
using DataAccess;

namespace GeoClinet.Pages.setdetail
{
    public class IndexModel : PageModel
    {
        private readonly DataAccess.GeoTycoonDbcontext _context;

        public IndexModel(DataAccess.GeoTycoonDbcontext context)
        {
            _context = context;
        }

        public IList<SetQuestionDetail> SetQuestionDetail { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool SearchByTitle { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool SearchBySetName { get; set; }

        public async Task OnGetAsync()
        {
            var query = _context.SetQuestionDetails
                .Include(s => s.Question)
                .Include(s => s.SetQuestion)
                .AsQueryable();

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                if (SearchByTitle && SearchBySetName)
                {
                    query = query.Where(s => s.Question.Title.Contains(SearchTerm) || s.SetQuestion.SetName.Contains(SearchTerm));
                }
                else if (SearchByTitle)
                {
                    query = query.Where(s => s.Question.Title.Contains(SearchTerm));
                }
                else if (SearchBySetName)
                {
                    query = query.Where(s => s.SetQuestion.SetName.Contains(SearchTerm));
                }
            }

            SetQuestionDetail = await query.ToListAsync();
        }
    }
}
