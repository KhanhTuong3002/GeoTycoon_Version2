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

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool SearchByEmail { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool SearchByFirstName { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool SearchByLastName { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchType { get; set; }

        public async Task OnGetAsync()
        {
            if (string.IsNullOrEmpty(SearchType))
            {
                SearchType = "Both"; // Default to searching by title
            }

            var profiles = from p in _context.Profiles.Include(p => p.User)
                           select p;


            if (!string.IsNullOrEmpty(SearchTerm))
            {
                if (SearchType == "SearchByEmail")
                {
                    profiles = profiles.Where(p => p.User.Email.Contains(SearchTerm));
                }
                if (SearchType == "SearchByFirstName")
                {
                    profiles = profiles.Where(p => p.FirstName.Contains(SearchTerm));
                }
                if (SearchType == "SearchByLastName")
                {
                    profiles = profiles.Where(p => p.LastName.Contains(SearchTerm));
                }
                if (SearchType == "Both")
                {
                    profiles = profiles.Where(p => p.User.Email.Contains(SearchTerm) || p.FirstName.Contains(SearchTerm) || p.LastName.Contains(SearchTerm));
                }
            }

            var profileList = await profiles.ToListAsync();

            Profile = new List<ProfileWithRoles>();

            foreach (var profile in profileList)
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
