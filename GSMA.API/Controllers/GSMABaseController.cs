using GSMA.API.Filters;
using GSMA.Logger;
using GSMA.Models.Request;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GSMA.API.Controllers
{
    [Route("api/[controller]")]
    [TypeFilter(typeof(UsageLog))]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class GSMABaseController : ControllerBase
    {
        public ILoggerManager logger;

        protected async Task<IActionResult> PerformOperation<T>(Request<T> request, Func<Task<IActionResult>> method)
        {
            IActionResult response = null;
            response = await method.Invoke();
            return response;
        }

        protected async Task<IActionResult> PerformOperation(int request, Func<Task<IActionResult>> method)
        {
            IActionResult response = null;
            response = await method.Invoke();
            return response;
        }
    }
}