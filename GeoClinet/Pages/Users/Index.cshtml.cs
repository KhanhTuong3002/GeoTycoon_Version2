﻿using System.Collections.Generic;
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
        public string SearchTerm { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool SearchByUserName { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool SearchByEmail { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchType { get; set; }


        public async Task OnGetAsync()
        {
            if (string.IsNullOrEmpty(SearchType))
            {
                SearchType = "Both"; // Default to searching by title
            }
            var usersQuery = _userManager.Users.AsQueryable();
            
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                if (SearchType == "SearchByUserName")
                {
                    usersQuery = usersQuery.Where(u => u.UserName.Contains(SearchTerm));
                }

                if (SearchType == "SearchByEmail")
                {
                    usersQuery = usersQuery.Where(u => u.Email.Contains(SearchTerm));
                }
                if (SearchType == "Both")
                {
                    usersQuery = usersQuery.Where(u => u.UserName.Contains(SearchTerm) || u.Email.Contains(SearchTerm));
                }
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
