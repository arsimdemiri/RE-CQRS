using RealEstate.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstate.Services
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(Guid Id);
        Task<List<User>> GetUsersInRole(string roleName);

    }
}
