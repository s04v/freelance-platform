using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.Users.Entities;

namespace Core.Users
{
    public interface IUserRepository
    {
        Task CreateUser(User user, CancellationToken token);
        Task<User?> GetUser(Expression<Func<User, bool>> where, CancellationToken token);
        Task<User?> GetUserByEmail(string email, CancellationToken token);
        Task<User?> GetUserByActivateToken(string? activateToken, CancellationToken token);
        Task<bool> CheckIfUserWithEmailExists(string email, CancellationToken token);
        Task Save(CancellationToken token);
    }
}
