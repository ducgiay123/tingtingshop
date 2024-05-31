using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopWeb.Core.src.Role;

namespace ShopWeb.Service.src.DTOs
{
    public class UserLoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class UserCreateDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Phone { get; set; }

        public string? AvatarLink { get; set; }
        // public UserRole Role { get; set; } 

    }
    public class UserReadDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }
        public string? AvatarLink { get; set; }
    }
    public class UserProfileDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }


    public class UserUpdateDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }

    }
}