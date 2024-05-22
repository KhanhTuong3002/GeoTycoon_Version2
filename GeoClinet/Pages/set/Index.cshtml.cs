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

        public IList<SetQuestion> SetQuestions { get; set; } = new List<SetQuestion>();

        [BindProperty]
        public SetQuestion SetQuestion { get; set; }

        public async Task OnGetAsync()
        {
            SetQuestions = await _context.SetQuestions.ToListAsync();
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
        private bool SetQuestionExists(string id)
        {
            return _context.SetQuestions.Any(e => e.Id == id);
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
    }
}
