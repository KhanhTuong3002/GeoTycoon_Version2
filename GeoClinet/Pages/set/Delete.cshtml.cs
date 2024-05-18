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
    public class DeleteModel : PageModel
    {
        private readonly DataAccess.GeoTycoonDbcontext _context;

        public DeleteModel(DataAccess.GeoTycoonDbcontext context)
        {
            _context = context;
        }

        [BindProperty]
        public SetQuestion SetQuestion { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setquestion = await _context.SetQuestions.FirstOrDefaultAsync(m => m.Id == id);

            if (setquestion == null)
            {
                return NotFound();
            }
            else
            {
                SetQuestion = setquestion;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setquestion = await _context.SetQuestions.FindAsync(id);
            if (setquestion != null)
            {
                SetQuestion = setquestion;
                _context.SetQuestions.Remove(SetQuestion);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
