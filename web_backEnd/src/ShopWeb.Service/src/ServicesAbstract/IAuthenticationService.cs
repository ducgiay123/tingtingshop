using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopWeb.Service.src.DTOs;

namespace ShopWeb.Service.src.ServicesAbstract
{
    public interface IAuthenticationService
    {
        Task<bool> RegisterServiceAsync(UserCreateDto request);
        Task<string> LoginServiceAsync(UserLoginDto userLoginDto);
        Task<UserReadDto> GetUserProfileFromTokenAsync(Guid id);

    }
}