using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShopWeb.Core.src.Common;
using ShopWeb.Core.src.Entity;
using ShopWeb.Service.src.DTOs;
using ShopWeb.Service.src.ServicesAbstract;

namespace ShopWeb.Service.src.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        private readonly IMapper _mapper;
        public AuthenticationService(IConfiguration configuration, IUserService userService, IMapper mapper)
        {
            _configuration = configuration;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<bool> RegisterServiceAsync(UserCreateDto userRequest)
        {

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(userRequest.Password);
            userRequest.Password = passwordHash;
            var rs = await _userService.CreateUserAsync(userRequest);
            return rs;
            // throw new NotImplementedException();
        }

        public async Task<string> LoginServiceAsync(UserLoginDto userLoginDto)
        {
            var rs = await _userService.GetUserByUserEmail(userLoginDto.Email);
            if (BCrypt.Net.BCrypt.Verify(userLoginDto.Password, rs.PasswordHash))
            {
                return CreateToken(rs);
            }
            throw AppException.Unauthorized("Invalid Email or Password");
        }
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JwtSettings:Key").Value!));

            // signing credentials
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(10),
                SigningCredentials = credentials,
                Issuer = _configuration.GetSection("JwtSettings:Issuer").Value!,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            // var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenHandler.WriteToken(token);
        }

        public async Task<UserReadDto> GetUserProfileFromTokenAsync(Guid userId )
        {

            var user = await _userService.GetUserByIdAsync(userId);
            return user;
        }
    }
}