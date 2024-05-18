using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject.Entites;
using DataAccess;

namespace GeoClinet.Pages.setdetail
{
    public class CreateModel : PageModel
    {
        private readonly DataAccess.GeoTycoonDbcontext _context;

        public CreateModel(DataAccess.GeoTycoonDbcontext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "Title");
        ViewData["SetQuestionId"] = new SelectList(_context.SetQuestions, "Id", "SetName");
            return Page();
        }

        [BindProperty]
        public SetQuestionDetail SetQuestionDetail { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {

            _context.SetQuestionDetails.Add(SetQuestionDetail);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
