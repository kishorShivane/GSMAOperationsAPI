using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GSMA.Logger;
using GSMA.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GSMA.API.Controllers
{
    [Route("api/[controller]")]
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