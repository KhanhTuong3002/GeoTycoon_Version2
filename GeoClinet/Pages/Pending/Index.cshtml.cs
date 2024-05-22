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

namespace GeoClinet.Pages.Pending
{
    public class IndexModel : PageModel
    {
        private readonly DataAccess.GeoTycoonDbcontext _context;
        private readonly UserManager<IdentityUser> userManager;

        public IndexModel(DataAccess.GeoTycoonDbcontext context, UserManager<IdentityUser> user)
        {
            _context = context;
            userManager = user;
        }

        public IList<IdentityUser> Profile { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool SearchByEmail { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool SearchByUserName { get; set; }

        public async Task OnGetAsync()
        {
            var users = await userManager.GetUsersInRoleAsync("Pending");
            var query = users.AsQueryable();

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                if (SearchByEmail || SearchByUserName)
                {
                    if (SearchByEmail)
                    {
                        query = query.Where(u => u.Email.Contains(SearchTerm));
                    }

                    if (SearchByUserName)
                    {
                        query = query.Where(u => u.UserName.Contains(SearchTerm));
                    }
                }
                else
                {
                    query = query.Where(u => u.Email.Contains(SearchTerm));
                }
            }

            Profile = query.ToList();
        }

        public async Task<IActionResult> OnPostAsync(string Id)
        {
            var user = await userManager.FindByIdAsync(Id);
            if (user != null)
            {
                await userManager.AddToRoleAsync(user, "Teacher");
                await userManager.RemoveFromRoleAsync(user, "Pending");
                return RedirectToPage();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
