using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopWeb.Core.src.Entity;
using ShopWeb.Service.src.DTOs;

namespace ShopWeb.Service.src.ServicesAbstract
{
    public interface IUserService
    {
        Task<IEnumerable<UserReadDto>> GetAllUsersAsync();
        /* Function to get one user by ID: */
        Task<UserReadDto> GetUserByIdAsync(Guid userId);
        /* Function to update a user: */
        Task<bool> UpdateUserByIdAsync(UserUpdateDto user);
        /* Function to delete a user: */
        Task<bool> DeleteUserByIdAsync(Guid id);
        /* Function to Create a user: */
        Task<bool> CreateUserAsync(UserCreateDto user);

        Task<User> GetUserByUserEmail(string userName);
    }
}