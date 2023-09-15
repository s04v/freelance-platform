using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IMailService
    {
        Task SendUserProfileActivate(string email, string activateToken);
        Task SendRecoveryPassword(string email, string recoveryToken, CancellationToken token);
    }
}
