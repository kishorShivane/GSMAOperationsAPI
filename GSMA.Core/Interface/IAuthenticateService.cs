using GSMA.Models;
using GSMA.Models.Request;
using GSMA.Models.Response;
using System.Threading.Tasks;

namespace GSMA.Core.Interface
{
    public interface IAuthenticateService
    {
        Task<Response<UserModel>> ValidateUserCredential(Request<UserModel> request);
    }
}
