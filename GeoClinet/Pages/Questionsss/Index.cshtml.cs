using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Entites;
using DataAccess;

namespace GeoClinet.Pages.Questionsss
{
    public class IndexModel : PageModel
    {
        private readonly DataAccess.GeoTycoonDbcontext _context;

        public IndexModel(DataAccess.GeoTycoonDbcontext context)
        {
            _context = context;
        }

        public IList<Question> Question { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool SearchByTitle { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool SearchByContent { get; set; }

        public async Task OnGetAsync()
        {
            // Default to SearchByTitle if no search type is selected
            if (!SearchByTitle && !SearchByContent)
            {
                SearchByTitle = true;
            }

            var questions = from q in _context.Questions
                            select q;

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                if (SearchByTitle && SearchByContent)
                {
                    questions = questions.Where(q => q.Title.Contains(SearchTerm) || q.Content.Contains(SearchTerm));
                }
                else if (SearchByTitle)
                {
                    questions = questions.Where(q => q.Title.Contains(SearchTerm));
                }
                else if (SearchByContent)
                {
                    questions = questions.Where(q => q.Content.Contains(SearchTerm));
                }
            }

            Question = await questions
                .Include(q => q.Province)
                .Include(q => q.User)
                .ToListAsync();
        }
        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            var questionToDelete = await _context.Questions.FindAsync(id);

            if (questionToDelete == null)
            {
                return NotFound();
            }

            _context.Questions.Remove(questionToDelete);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}
