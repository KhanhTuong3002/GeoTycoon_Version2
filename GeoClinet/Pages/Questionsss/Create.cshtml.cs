using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject.Entites;
using DataAccess;

namespace GeoClinet.Pages.Questionsss
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
        ViewData["ProvinceId"] = new SelectList(_context.Set<Province>(), "Id", "ProvinceName");
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Question Question { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {

            _context.Questions.Add(Question);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
