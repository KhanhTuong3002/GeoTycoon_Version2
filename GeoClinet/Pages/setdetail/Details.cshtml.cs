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

namespace GeoClinet.Pages.setdetail
{
    [Authorize(Policy = "Teacher")]
    public class DetailsModel : PageModel
    {
        private readonly DataAccess.GeoTycoonDbcontext _context;

        public DetailsModel(DataAccess.GeoTycoonDbcontext context)
        {
            _context = context;
        }

        public SetQuestionDetail SetQuestionDetail { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setquestiondetail = await _context.SetQuestionDetails.FirstOrDefaultAsync(m => m.Id == id);
            if (setquestiondetail == null)
            {
                return NotFound();
            }
            else
            {
                SetQuestionDetail = setquestiondetail;
            }
            return Page();
        }
    }
}
