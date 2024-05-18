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

namespace GeoClinet.Pages.setdetail
{
    public class EditModel : PageModel
    {
        private readonly DataAccess.GeoTycoonDbcontext _context;

        public EditModel(DataAccess.GeoTycoonDbcontext context)
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

            var setquestiondetail =  await _context.SetQuestionDetails.FirstOrDefaultAsync(m => m.Id == id);
            if (setquestiondetail == null)
            {
                return NotFound();
            }
            SetQuestionDetail = setquestiondetail;
           ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "Id");
           ViewData["SetQuestionId"] = new SelectList(_context.SetQuestions, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(SetQuestionDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SetQuestionDetailExists(SetQuestionDetail.Id))
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

        private bool SetQuestionDetailExists(string id)
        {
            return _context.SetQuestionDetails.Any(e => e.Id == id);
        }
    }
}
