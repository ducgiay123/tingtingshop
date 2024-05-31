using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Castle.Core.Configuration;
using Microsoft.Extensions.Configuration;
using Moq;
using ShopWeb.Core.src.Common;
using ShopWeb.Core.src.Entity;
using ShopWeb.Core.src.Role;
using ShopWeb.Service.src.DTOs;
using ShopWeb.Service.src.Services;
using ShopWeb.Service.src.ServicesAbstract;
using Xunit;

namespace ShopWeb.Tests.Service
{
    public class AuthenticationServiceTests
    {
        private readonly AuthenticationService _authenticationService;
        private readonly Mock<IUserService> _userServiceMock;

        public AuthenticationServiceTests()
        {
            _userServiceMock = new Mock<IUserService>();

            var configuration = new ConfigurationBuilder().Build(); // You may need to set up IConfiguration if needed
            var mapper = new Mock<IMapper>().Object;

            _authenticationService = new AuthenticationService(configuration, _userServiceMock.Object, mapper);
        }

        [Fact]
        public async Task RegisterServiceAsync_ValidRequest_ReturnsTrue()
        {
            // Arrange
            var userCreateDto = new UserCreateDto
            {
                // Set up userCreateDto
            };

            _userServiceMock.Setup(x => x.CreateUserAsync(It.IsAny<UserCreateDto>())).ReturnsAsync(true);

            // Act
            var result = await _authenticationService.RegisterServiceAsync(userCreateDto);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task LoginServiceAsync_ValidCredentials_ReturnsToken()
        {
            // Arrange
            var userLoginDto = new UserLoginDto
            {
                // Set up userLoginDto
            };

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = "test@example.com",
                Role = UserRole.Customer,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("password") // Assume password is hashed during registration
            };

            _userServiceMock.Setup(x => x.GetUserByUserEmail(It.IsAny<string>())).ReturnsAsync(user);

            // Act
            var result = await _authenticationService.LoginServiceAsync(userLoginDto);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task LoginServiceAsync_InvalidCredentials_ThrowsException()
        {
            // Arrange
            var userLoginDto = new UserLoginDto
            {
                // Set up userLoginDto
            };

            _userServiceMock.Setup(x => x.GetUserByUserEmail(It.IsAny<string>())).ReturnsAsync((User)null); // User not found

            // Act & Assert
            await Assert.ThrowsAsync<AppException>(() => _authenticationService.LoginServiceAsync(userLoginDto));
        }

        [Fact]
        public async Task GetUserProfileFromTokenAsync_ValidUserId_ReturnsUserReadDto()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User
            {
                // Set up user
            };


            // Act
            var result = await _authenticationService.GetUserProfileFromTokenAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Id, result.Id);
            // Assert other properties as needed
        }
    }
}
