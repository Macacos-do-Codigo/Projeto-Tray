using Microsoft.EntityFrameworkCore;
using Infrastructure.Repositories.Data;
using Core.Models;
using Core.Repositories;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly OrdersDbContext _context;

        public UserRepository(OrdersDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUser(string userId)
        {
            return await _context.Users.FirstOrDefaultAsync(c => c.Id == userId);
        }

        public async Task<User> SignUp(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }
    }
}
