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
            var setQuestion = await _context.SetQuestions.FindAsync(SetQuestionDetail.SetQuestionId);

            if (setQuestion == null)
            {
                ModelState.AddModelError(string.Empty, "SetQuestion không tồn tại.");
                return Page();
            }

            var existingQuestionsCount = _context.SetQuestionDetails
                                                 .Count(sqd => sqd.SetQuestionId == SetQuestionDetail.SetQuestionId);

            if (existingQuestionsCount >= setQuestion.QuestionNumber)
            {
                ModelState.AddModelError(string.Empty, "Số lượng câu hỏi đã vượt quá số lượng cho phép.");
                return Page();
            }
            _context.SetQuestionDetails.Add(SetQuestionDetail);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
