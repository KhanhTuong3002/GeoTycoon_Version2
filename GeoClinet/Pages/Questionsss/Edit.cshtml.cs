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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace GeoClinet.Pages.Questionsss
{
    [Authorize(Policy = "Teacher")]
    public class EditModel : PageModel
    {
        private readonly DataAccess.GeoTycoonDbcontext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public EditModel(DataAccess.GeoTycoonDbcontext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Question Question { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions.FirstOrDefaultAsync(m => m.Id == id);
            if (question == null)
            {
                return NotFound();
            }
            Question = question;
            ViewData["ProvinceId"] = new SelectList(_context.Set<Province>(), "Id", "ProvinceName");
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (Question.Option1 == Question.Option2 ||
                Question.Option1 == Question.Option3 ||
                Question.Option1 == Question.Option4 ||
                Question.Option2 == Question.Option3 ||
                Question.Option2 == Question.Option4 ||
               (Question.Option3 != null && Question.Option3 == Question.Option4))
            {
                ModelState.AddModelError("Question.Option3", "Options cannot be duplicated.");
                return Page();
            }
            _context.Attach(Question).State = EntityState.Modified;
            Question.UserId = currentUser?.Id;

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