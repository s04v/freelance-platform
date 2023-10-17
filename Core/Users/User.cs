using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Core.Users
{
    public class User
    {
        public Guid Uuid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Description { get; set; }
        public string? ActivateToken { get; set; }
        public string? RecoveryToken { get; set; }
        public bool IsActive { get; set; }
        public string? Photo { get; set; }
        public string? NewEmail { get; set; }
    }
}
