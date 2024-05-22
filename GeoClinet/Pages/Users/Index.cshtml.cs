using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GeoClinet.Pages
{
    public class UserListModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserListModel(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IList<UserWithRoles> Users { get; set; }

        public async Task OnGetAsync()
        {
            var users = _userManager.Users.ToList();
            var userWithRolesList = new List<UserWithRoles>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userWithRolesList.Add(new UserWithRoles
                {
                    User = user,
                    Roles = roles
                });
            }

            Users = userWithRolesList;
        }
    }

    public class UserWithRoles
    {
        public IdentityUser User { get; set; }
        public IList<string> Roles { get; set; }
    }
}
