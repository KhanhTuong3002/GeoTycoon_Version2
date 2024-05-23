using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using BusinessObject.Entites;
using DataAccess;
using Microsoft.AspNetCore.Authorization;

namespace GeoClinet.Pages.Profile123
{
    [Authorize(Policy = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly GeoTycoonDbcontext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(GeoTycoonDbcontext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<ProfileWithRoles> Profile { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var profiles = await _context.Profiles
                .Include(p => p.User)
                .ToListAsync();

            Profile = new List<ProfileWithRoles>();

            foreach (var profile in profiles)
            {
                var userRoles = await _userManager.GetRolesAsync(profile.User);
                Profile.Add(new ProfileWithRoles
                {
                    Profile = profile,
                    Roles = userRoles
                });
            }
        }

        public async Task<IActionResult> OnPostBanAsync(string id)
        {
            var profile = await _context.Profiles.FindAsync(id);
            if (profile == null)
            {
                return NotFound();
            }

            profile.Isbanned = true;
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostUnbanAsync(string id)
        {
            var profile = await _context.Profiles.FindAsync(id);
            if (profile == null)
            {
                return NotFound();
            }

            profile.Isbanned = false;
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }

    public class ProfileWithRoles
    {
        public Profile Profile { get; set; }
        public IList<string> Roles { get; set; }
    }
}
