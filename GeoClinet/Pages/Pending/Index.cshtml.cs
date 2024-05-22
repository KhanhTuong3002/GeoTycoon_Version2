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

        public IList<IdentityUser> Profile { get;set; } = default!;
        public async Task OnGet()
        {
            Profile = await userManager.GetUsersInRoleAsync("Pending");
        }
        public async Task<IActionResult> OnPost(string Id)
        {
            var user = await userManager.FindByIdAsync(Id);
            if (user != null)
            {
                await userManager.AddToRoleAsync(user, "Teacher");
                await userManager.RemoveFromRoleAsync(user, "Pending");
                return RedirectToPage("Index");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
