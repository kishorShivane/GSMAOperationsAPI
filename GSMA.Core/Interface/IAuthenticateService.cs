using GSMA.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GSMA.Core.Interface
{
    public interface IAuthenticateService
    {
        Task<UserModel> ValidateUserCredential(UserModel user);
    }
}
