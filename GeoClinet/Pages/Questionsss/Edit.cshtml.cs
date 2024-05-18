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

namespace GeoClinet.Pages.Questionsss
{
    public class EditModel : PageModel
    {
        private readonly DataAccess.GeoTycoonDbcontext _context;

        public EditModel(DataAccess.GeoTycoonDbcontext context)
        {
            _context = context;
        }

        [BindProperty]
        public Question Question { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question =  await _context.Questions.FirstOrDefaultAsync(m => m.Id == id);
            if (question == null)
            {
                return NotFound();
            }
            Question = question;
           ViewData["ProvinceId"] = new SelectList(_context.Set<Province>(), "Id", "ProvinceName");
           ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {


            _context.Attach(Question).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionExists(Question.Id))
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

        private bool QuestionExists(string id)
        {
            return _context.Questions.Any(e => e.Id == id);
        }
    }
}
