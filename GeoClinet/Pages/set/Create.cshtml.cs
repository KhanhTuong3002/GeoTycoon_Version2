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
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace GeoClinet.Pages.set
{
    [Authorize(Policy = "Teacher")]
    public class CreateModel : PageModel
    {
        private readonly DataAccess.GeoTycoonDbcontext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateModel(DataAccess.GeoTycoonDbcontext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public SetQuestion SetQuestion { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                ModelState.AddModelError("", "ID not found.");
                return Page();
            }

            var existingSetQuestion = await _context.SetQuestions
            .FirstOrDefaultAsync(sq => sq.SetName == SetQuestion.SetName);

            if (existingSetQuestion != null)
            {
                ModelState.AddModelError("SetQuestion.SetName", "SetName is ready exits.");
                return Page();
            }
            _context.SetQuestions.Add(SetQuestion);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
