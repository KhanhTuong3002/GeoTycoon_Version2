using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Entites;
using DataAccess;
using Microsoft.AspNetCore.Authorization;

namespace GeoClinet.Pages.Questionsss
{
    [Authorize(Policy = "Teacher")]
    //[Authorize(Policy = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly DataAccess.GeoTycoonDbcontext _context;

        public IndexModel(DataAccess.GeoTycoonDbcontext context)
        {
            _context = context;
        }

        public IList<Question> Question { get; set; } = default!;
        public IList<Province> Provinces { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool SearchByTitle { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool SearchByContent { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SelectedProvinceName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchType { get; set; }

        public async Task OnGetAsync()
        {
            // Load all provinces for the dropdown
            Provinces = await _context.Provinces.ToListAsync();

            // Default SearchType if not set
            if (string.IsNullOrEmpty(SearchType))
            {
                SearchType = "Both"; // Default to searching by title
            }

            var questions = _context.Questions.Include(q => q.Province).Include(q => q.User).AsQueryable();

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                if (SearchType == "Title")
                {
                    questions = questions.Where(q => q.Title.Contains(SearchTerm));
                }
                else if (SearchType == "Content")
                {
                    questions = questions.Where(q => q.Content.Contains(SearchTerm));
                }
                else if (SearchType == "Both")
                {
                    questions = questions.Where(q => q.Title.Contains(SearchTerm) || q.Content.Contains(SearchTerm));
                }
            }

            if (!string.IsNullOrEmpty(SelectedProvinceName))
            {
                questions = questions.Where(q => q.Province.ProvinceName.Contains(SelectedProvinceName));
            }

            Question = await questions.ToListAsync();
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
