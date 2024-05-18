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
    public class DeleteModel : PageModel
    {
        private readonly DataAccess.GeoTycoonDbcontext _context;

        public DeleteModel(DataAccess.GeoTycoonDbcontext context)
        {
            _context = context;
        }

        [BindProperty]
        public SetQuestionDetail SetQuestionDetail { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setquestiondetail = await _context.SetQuestionDetails.FirstOrDefaultAsync(m => m.Id == id);

            if (setquestiondetail == null)
            {
                return NotFound();
            }
            else
            {
                SetQuestionDetail = setquestiondetail;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setquestiondetail = await _context.SetQuestionDetails.FindAsync(id);
            if (setquestiondetail != null)
            {
                SetQuestionDetail = setquestiondetail;
                _context.SetQuestionDetails.Remove(SetQuestionDetail);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
