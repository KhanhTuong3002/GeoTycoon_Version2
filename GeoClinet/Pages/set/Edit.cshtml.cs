using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Entites;
using DataAccess;

namespace GeoClinet.Pages.set
{
    public class EditModel : PageModel
    {
        private readonly DataAccess.GeoTycoonDbcontext _context;

        public EditModel(DataAccess.GeoTycoonDbcontext context)
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

            var setquestion =  await _context.SetQuestions.FirstOrDefaultAsync(m => m.Id == id);
            if (setquestion == null)
            {
                return NotFound();
            }
            SetQuestion = setquestion;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
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

            return RedirectToPage("./Index");
        }

        private bool SetQuestionExists(string id)
        {
            return _context.SetQuestions.Any(e => e.Id == id);
        }
    }
}
