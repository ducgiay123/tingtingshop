using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopWeb.Core.src.Entity;

namespace ShopWeb.Core.src.RepoAbstract
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        /* Function to get one user by ID: */
        Task<User> GetUserByIdAsync(Guid id);
        Task<User> GetUserByEmailAsync(string email);
        /* Function to update a user: */
        Task<bool> UpdateUserByIdAsync(User user);
        /* Function to delete a user: */
        Task<bool> DeleteUserByIdAsync(Guid id);
        /* Function to Create a user: */
        Task<bool> CreateUserAsync(User user);
    }
}