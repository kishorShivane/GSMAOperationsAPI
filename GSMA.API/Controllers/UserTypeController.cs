using GSMA.API.Filters;
using GSMA.Core.Interface;
using GSMA.Logger;
using GSMA.Models;
using GSMA.Models.Request;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GSMA.API.Controllers
{
    [Route("api/[controller]")]
    [TypeFilter(typeof(UsageLog))]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class UserTypeController : GSMABaseController
    {
        IService<UserTypeModel> service;
        public UserTypeController(ILoggerManager _logger, IService<UserTypeModel> _service)
        {
            logger = _logger;
            service = _service;
        }

        [Route("GetUserTypes")]
        [HttpPost]
        public async Task<IActionResult> Get([FromBody] Request<UserTypeModel> request)
        {
            return await PerformOperation<UserTypeModel>(request, async () =>
            {
                var response = await service.Get(request);
                return Ok(response);
            });
        }

        [Route("InsertUserType")]
        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] Request<UserTypeModel> request)
        {
            return await PerformOperation<UserTypeModel>(request, async () =>
            {
                var response = await service.Insert(request);
                return Ok(response);
            });
        }

        [Route("UploadUserTypes")]
        [HttpPost]
        public async Task<IActionResult> InsertAll([FromBody] Request<List<UserTypeModel>> request)
        {
            return await PerformOperation<List<UserTypeModel>>(request, async () =>
            {
                var response = await service.InsertAll(request);
                return Ok(response);
            });
        }

        [Route("UpdateUserType")]
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] Request<UserTypeModel> request)
        {
            return await PerformOperation<UserTypeModel>(request, async () =>
            {
                var response = await service.Update(request);
                return Ok(response);
            });
        }

        [Route("DeleteUserType")]
        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            return await PerformOperation(id, async () =>
            {
                var response = await service.Delete(id);
                return Ok(response);
            });
        }
    }
}