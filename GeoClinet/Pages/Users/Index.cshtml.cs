using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GeoClinet.Pages
{
    public class UserListModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserListModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public IList<IdentityUser> Users { get; set; }

        public void OnGet()
        {
            Users = _userManager.Users.ToList();
        }
    }
}
