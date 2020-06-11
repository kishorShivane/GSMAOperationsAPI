using GSMA.API.Filters;
using GSMA.Core.Interface;
using GSMA.Core.Utilities;
using GSMA.Infrastructure.Security.Services;
using GSMA.Logger;
using GSMA.Models;
using GSMA.Models.Request;
using GSMA.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace GSMA.API.Controllers
{
    [Route("api/[controller]")]
    [TypeFilter(typeof(UsageLog))]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private ILoggerManager logger;
        private IConfiguration config;
        private IAuthenticateService service;

        public AuthenticationController(ILoggerManager _logger, IConfiguration _config, IAuthenticateService _service)
        {
            config = _config;
            service = _service;
            logger = _logger;
        }

        [Route("AuthenticateUser")]
        [HttpPost]
        public async Task<IActionResult> AuthenticateUser(Request<UserModel> request)
        {
            IActionResult response;
            Response<UserModel> responseContent = new Response<UserModel>();
            string requestString = JsonConvert.SerializeObject(request);
            if (request != null && request.Entity != null)
            {
                var isValidUser = await service.ValidateUserCredential(request);
                if (isValidUser != null && isValidUser.ResultSet != null)
                {
                    var jwt = new JwtTokenService(config);
                    var token = jwt.GenerateSecurityToken(isValidUser.ResultSet);
                    isValidUser.ResultSet.Token = token;
                    isValidUser.ResultSet.Password = "";
                    responseContent = isValidUser;
                    response = Ok(responseContent);
                    logger.LogInfo(MessageConstant.AUTHENTICATION_SUCCESSFUL + " Request: " + requestString);
                }
                else
                {
                    response = Unauthorized(MessageConstant.AUTHENTICATION_FAILED);
                    logger.LogError(MessageConstant.AUTHENTICATION_FAILED + " Request: " + requestString);
                }
            }
            else
            {
                responseContent.AddErrorMessage(MessageConstant.GENERAL_INVALID_ARGUMENT);
                response = Ok(responseContent);
            }
            return response;
        }
    }
}