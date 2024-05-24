using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GeoClinet.Pages
{
    [Authorize(Policy = "Admin")]
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

        [BindProperty(SupportsGet = true)]
        public string SearchUserName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchEmail { get; set; }

        public async Task OnGetAsync()
        {
            var usersQuery = _userManager.Users.AsQueryable();

            if (!string.IsNullOrEmpty(SearchUserName))
            {
                usersQuery = usersQuery.Where(u => u.UserName.Contains(SearchUserName));
            }

            if (!string.IsNullOrEmpty(SearchEmail))
            {
                usersQuery = usersQuery.Where(u => u.Email.Contains(SearchEmail));
            }

            var users = await usersQuery.ToListAsync();
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
