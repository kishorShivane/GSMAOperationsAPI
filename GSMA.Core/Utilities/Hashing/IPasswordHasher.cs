using System;
using System.Collections.Generic;
using System.Text;

namespace GSMA.Core.Utilities.Hashing
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool PasswordMatches(string providedPassword, string passwordHash);
    }
}
