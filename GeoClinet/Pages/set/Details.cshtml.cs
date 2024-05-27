using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Entites;
using DataAccess;
using Microsoft.AspNetCore.Authorization;

namespace GeoClinet.Pages.set
{
    [Authorize(Policy = "Teacher")]
    public class DetailsModel : PageModel
    {
        private readonly DataAccess.GeoTycoonDbcontext _context;

        public DetailsModel(DataAccess.GeoTycoonDbcontext context)
        {
            _context = context;
        }

        public SetQuestion SetQuestion { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setquestion = await _context.SetQuestions.FirstOrDefaultAsync(m => m.Id == id);
            if (setquestion == null)
            {
                return NotFound();
            }
            else
            {
                SetQuestion = setquestion;
            }
            return Page();
        }
    }
}
