using Core.Users;
using Core.Users.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Database.Users
{
    public class UserRepository : IUserRepository
    {
        public BaseDbContext _dbContext;

        public UserRepository(BaseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateUser(User user, CancellationToken token)
        {
            await _dbContext.AddAsync(user, token);
            await _dbContext.SaveChangesAsync(token);
        }

        public async Task<User?> GetUser(Expression<Func<User, bool>> predicate, CancellationToken token)
        {
            return await _dbContext.User
                .Where(predicate)
                .FirstOrDefaultAsync();
        }

        public async Task<User?> GetUserByEmail(string email, CancellationToken token)
        {
            return await _dbContext.User
                        .Where(o => o.Email == email)
                        .FirstOrDefaultAsync(token);
        }

         
        public async Task<User?>  GetUserByActivateToken(string? activateToken, CancellationToken token)
        {
            return await _dbContext.User
                        .Where(o => o.ActivateToken == activateToken)
                        .FirstOrDefaultAsync(token);
        }

        public async Task<bool> CheckIfUserWithEmailExists(string email, CancellationToken token)
        {
            return await _dbContext.User
                        .AnyAsync(o => o.Email == email, token);
        }

        public async Task Save(CancellationToken token)
        {
            await _dbContext.SaveChangesAsync(token);
        }
    }
}
