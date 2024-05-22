using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Entites;
using DataAccess;

namespace GeoClinet.Pages.Profile123
{
    public class DetailsModel : PageModel
    {
        private readonly DataAccess.GeoTycoonDbcontext _context;

        public DetailsModel(DataAccess.GeoTycoonDbcontext context)
        {
            _context = context;
        }

        public Profile Profile { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profile = await _context.Profiles.FirstOrDefaultAsync(m => m.Id == id);
            if (profile == null)
            {
                return NotFound();
            }
            else
            {
                Profile = profile;
            }
            return Page();
        }
    }
}
