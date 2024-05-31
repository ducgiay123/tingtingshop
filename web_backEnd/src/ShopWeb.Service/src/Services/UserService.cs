using AutoMapper;
using ShopWeb.Core.src.Entity;
using ShopWeb.Core.src.RepoAbstract;
using ShopWeb.Core.src.Role;
using ShopWeb.Service.src.DTOs;
using ShopWeb.Service.src.ServicesAbstract;

namespace ShopWeb.Service.src.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        // public Task<UserReadDto> CreateUserAsync(UserLoginDto user)
        // {
        //     throw new NotImplementedException();
        // }

        // public async Task<UserReadDto> CreateAdminAsync(UserCreateDto userDto)
        // {
        //     var user = userDto.CreateAdmin(new User());
        //     var createdUser = await _userRepository.CreateUserAsync(user);
        //     var userReadDto = new UserReadDto();
        //     userReadDto.Transform(createdUser);
        //     return userReadDto;
        // }
        public async Task<bool> CreateUserAsync(UserCreateDto userDto)
        {

            var user = _mapper.Map<User>(userDto);
            user.Role = UserRole.Customer;
            var rs = await _userRepository.CreateUserAsync(user);
            return rs;
        }

        public async Task<bool> DeleteUserByIdAsync(Guid id)
        {
            return await _userRepository.DeleteUserByIdAsync(id);
        }

        public async Task<IEnumerable<UserReadDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            var usersDto = _mapper.Map<IEnumerable<UserReadDto>>(users);
            return usersDto;
        }

        // public Task<IEnumerable<UserReadDto>> GetAllUsersAsync(UserQueryOptions options)
        // {
        //     throw new NotImplementedException();
        // }
        public async Task<User> GetUserByUserEmail(string emmail)
        {
            return await _userRepository.GetUserByEmailAsync(emmail);
        }
        public async Task<UserReadDto> GetUserByIdAsync(Guid userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            var userDto = _mapper.Map<UserReadDto>(user);
            return userDto;
        }

        public async Task<bool> UpdateUserByIdAsync(UserUpdateDto userDto)
        {
            throw new NotImplementedException();
            // var existingUser = await _userRepository.GetUserByIdAsync(userDto.Id);
            // if (existingUser == null) return false;

            // existingUser = userDto.UpdateUser(existingUser);
            // return await _userRepository.UpdateUserByIdAsync(existingUser);
        }
    }
}