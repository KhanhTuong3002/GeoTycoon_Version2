using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Entites;
using DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace GeoClinet.Pages.Pending
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

        public IList<IdentityUser> Profile { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool SearchByUsername { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool SearchByEmail { get; set; }

        public async Task OnGetAsync()
        {
            // Default to SearchByUsername if no search type is selected
            if (!SearchByUsername && !SearchByEmail)
            {
                SearchByUsername = true;
            }

            var usersInRole = await _userManager.GetUsersInRoleAsync("Pending");
            var profiles = usersInRole.AsQueryable();

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                if (SearchByUsername && SearchByEmail)
                {
                    profiles = profiles.Where(u => u.UserName.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase) || u.Email.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase));
                }
                else if (SearchByUsername)
                {
                    profiles = profiles.Where(u => u.UserName.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase));
                }
                else if (SearchByEmail)
                {
                    profiles = profiles.Where(u => u.Email.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase));
                }
            }

            Profile = profiles.ToList();
        }

        public async Task<IActionResult> OnPostAsync(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if (user != null)
            {
                await _userManager.AddToRoleAsync(user, "Teacher");
                await _userManager.RemoveFromRoleAsync(user, "Pending");
                return RedirectToPage("Index");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
