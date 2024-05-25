using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject.Entites;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace GeoClinet.Pages.Questionsss
{
    public class CreateModel : PageModel
    {
        private readonly DataAccess.GeoTycoonDbcontext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public CreateModel(DataAccess.GeoTycoonDbcontext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
        ViewData["ProvinceId"] = new SelectList(_context.Set<Province>(), "Id", "ProvinceName");
        //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Question Question { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var existingSetQuestion = await _context.Questions
            .FirstOrDefaultAsync(sq => sq.Title == Question.Title);

            if (existingSetQuestion != null)
            {
                ModelState.AddModelError("Question.Title", "Title already exists.");
                return Page();
            }
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
            Question.UserId = currentUser?.Id;
            _context.Questions.Add(Question);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
