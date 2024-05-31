using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopWeb.API.Database;
using ShopWeb.Core.src.Entity;
using ShopWeb.Core.src.RepoAbstract;

namespace ShopWeb.API.Repo
{
    public class UserRepository : IUserRepository
    {
        private readonly WebShopDbContext _context;

        public UserRepository(WebShopDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateUserAsync(User user)
        {

            var existingUser = await _context.User.FirstOrDefaultAsync(u => u.Email == user.Email);

            if (existingUser != null)
            {
                return false;
            }
            user.Id = Guid.NewGuid();
            _context.User.Add(user); // create a snapshot
            await _context.SaveChangesAsync(); // update actual database (create, update, delete)

            return true; // User created successfully
        }

        public async Task<bool> DeleteUserByIdAsync(Guid id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
                return false;

            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        // public async Task<IEnumerable<User>> GetAllUsersAsync(UserQueryOptions options)
        // {
        //     var query = _context.User.AsQueryable();

        //     // Implementing pagination
        //     if (options.Page > 0 && options.PageSize > 0)
        //         query = query.Skip((options.Page - 1) * options.PageSize).Take(options.PageSize);

        //     // Implementing sorting
        //     if (!string.IsNullOrWhiteSpace(options.SortBy))
        //     {
        //         switch (options.SortBy.ToLower())
        //         {
        //             case "firstname":
        //                 query = options.SortOrder.ToLower() == "asc" ? query.OrderBy(u => u.FirstName) : query.OrderByDescending(u => u.FirstName);
        //                 break;
        //             case "lastname":
        //                 query = options.SortOrder.ToLower() == "asc" ? query.OrderBy(u => u.LastName) : query.OrderByDescending(u => u.LastName);
        //                 break;
        //             // Add more sorting options if needed
        //             default:
        //                 break;
        //         }
        //     }

        //     // Implementing filtering
        //     if (!string.IsNullOrWhiteSpace(options.SearchKey))
        //         query = query.Where(u => u.FirstName.Contains(options.SearchKey) || u.LastName.Contains(options.SearchKey));

        //     return await query.ToListAsync();
        // }
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.User.ToListAsync();
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return _context.User.FirstOrDefault(user => user.Email == email);
        }

        // public async Task<User?> GetUserByCredentialsAsync(UserCredential userCredential)
        // {
        //     var foundUser = await _context.Users.FirstOrDefaultAsync(user => user.Email == userCredential.Email && user.Password == userCredential.Password);
        //     return foundUser;
        // }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            var rs = await _context.User.FindAsync(id);
            if (rs is null)
            {

            }
            return rs;
        }

        public async Task<bool> UpdateUserByIdAsync(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

    }
}