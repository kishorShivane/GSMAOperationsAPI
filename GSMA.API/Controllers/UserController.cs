using GSMA.API.Filters;
using GSMA.Core.Interface;
using GSMA.Logger;
using GSMA.Models;
using GSMA.Models.Request;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GSMA.API.Controllers
{
    [Route("api/[controller]")]
    [TypeFilter(typeof(UsageLog))]
    [ApiController]
    public class UserController : GSMABaseController
    {
        IService<UserModel> service;
        public UserController(ILoggerManager _logger, IService<UserModel> _service)
        {
            logger = _logger;
            service = _service;
        }

        [Route("GetUsers")]
        [HttpPost]
        public async Task<IActionResult> Get([FromBody] Request<UserModel> request)
        {
            return await PerformOperation<UserModel>(request, async () =>
            {
                var response = await service.Get(request);
                return Ok(response);
            });
        }

        [Route("InsertUser")]
        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] Request<UserModel> request)
        {
            return await PerformOperation<UserModel>(request, async () =>
            {
                var response = await service.Insert(request);
                return Ok(response);
            });
        }

        [Route("UploadUsers")]
        [HttpPost]
        public async Task<IActionResult> InsertAll([FromBody] Request<List<UserModel>> request)
        {
            return await PerformOperation<List<UserModel>>(request, async () =>
            {
                var response = await service.InsertAll(request);
                return Ok(response);
            });
        }

        [Route("UpdateUser")]
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] Request<UserModel> request)
        {
            return await PerformOperation<UserModel>(request, async () =>
            {
                var response = await service.Update(request);
                return Ok(response);
            });
        }

        [Route("DeleteUser")]
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