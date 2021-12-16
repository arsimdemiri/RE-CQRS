using Microsoft.AspNetCore.Identity;
using RealEstate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<User> GetUserByIdAsync(Guid Id)
        {
            return await _userManager.FindByIdAsync(Id.ToString());
        }

        public async Task<List<User>> GetUsersInRole(string roleName)
        {
            var users = await _userManager.GetUsersInRoleAsync(roleName);
            return users.ToList();
        }
    }
}
