using Core.DTOs;
using Core.Models;
using Core.Repositories;
using Core.Services;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        private async Task<User?> GetUser(string userId)
        {
            User? user = await _userRepository.GetUser(userId);

            return user;
        }

        public async Task<User> GetUserOrThrowException(string userId)
        {
            User? user = await GetUser(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            return user;
        }

        public async Task<User> SignUp(SignUpRequestDTO signUpRequestDTO)
        {
            User user = new User(
               email: signUpRequestDTO.Email,
               password: signUpRequestDTO.Password
           );

            user = await _userRepository.SignUp(user);
            return user;
        }
    }
}
