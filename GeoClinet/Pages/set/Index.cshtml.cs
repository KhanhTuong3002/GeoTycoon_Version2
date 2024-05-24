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

namespace GeoClinet.Pages.set
{
    [Authorize(Policy = "Teacher")]
    public class IndexModel : PageModel
    {
        private readonly GeoTycoonDbcontext _context;

        public IndexModel(GeoTycoonDbcontext context)
        {
            _context = context;
        }

        public IList<SetQuestion> SetQuestions { get; set; } = new List<SetQuestion>();

        [BindProperty]
        public SetQuestion SetQuestion { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchSetName { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? SearchQuestionNumber { get; set; }

        public async Task OnGetAsync()
        {
            var query = _context.SetQuestions.AsQueryable();

            if (!string.IsNullOrEmpty(SearchSetName))
            {
                query = query.Where(s => s.SetName.Contains(SearchSetName));
            }

            if (SearchQuestionNumber.HasValue)
            {
                query = query.Where(s => s.QuestionNumber == SearchQuestionNumber);
            }

            SetQuestions = await query.ToListAsync();
        }

        public async Task<IActionResult> OnPostAddAsync()
        {
            _context.SetQuestions.Add(SetQuestion);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditAsync(string id)
        {
            _context.Attach(SetQuestion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SetQuestionExists(SetQuestion.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            var questionToDelete = await _context.SetQuestions.FindAsync(id);

            if (questionToDelete == null)
            {
                return NotFound();
            }

            _context.SetQuestions.Remove(questionToDelete);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }

        private bool SetQuestionExists(string id)
        {
            return _context.SetQuestions.Any(e => e.Id == id);
        }
    }
}
