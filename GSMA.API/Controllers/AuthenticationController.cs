using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GSMA.Core.Interface;
using GSMA.Core.Utilities;
using GSMA.DataProvider.Data;
using GSMA.Infrastructure.Security.Services;
using GSMA.Models;
using GSMA.Models.Request;
using GSMA.Models.Response;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace GSMA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private IConfiguration config;
        private IAuthenticateService service;

        public AuthenticationController(IConfiguration _config, IAuthenticateService _service)
        {
            config = _config;
            service = _service;
        }

        [Route("AuthenticateUser")]
        [HttpPost]
        public async Task<IActionResult> AuthenticateUser(Request<UserModel> request)
        {
            IActionResult response;
            Response<UserModel> responseContent = new Response<UserModel>();
            if (request != null && request.Entity != null)
            {
                var validUser = await service.ValidateUserCredential(request.Entity);
                if (validUser != null)
                {
                    var jwt = new JwtTokenService(config);
                    var token = jwt.GenerateSecurityToken(validUser);
                    validUser.Token = token;
                    validUser.Password = "";
                    responseContent.ResultSet = validUser;
                    response = Ok(responseContent);
                }
                else
                {
                    response = Unauthorized(MessageConstant.AUTHENTICATION_FAILED);
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