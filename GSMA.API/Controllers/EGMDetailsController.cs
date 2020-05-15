using GSMA.API.Filters;
using GSMA.Logger;
using GSMA.Models;
using GSMA.Models.Request;
using GSMA.Models.Response;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using GSMA.Core.Interface;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace GSMA.API.Controllers
{
    [Route("api/[controller]")]
    [UsageLog]
    [ApiController]
    public class EGMDetailsController : GSMABaseController
    {
        IService<EGMDetailModel> service;
        public EGMDetailsController(ILoggerManager _logger, IService<EGMDetailModel> _service)
        {
            logger = _logger;
            service = _service;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("TestLog")]
        [HttpGet]
        public async Task<string> Test(string name)
        {
            await Task.Run(() =>
            {
                logger.LogInfo("Hello " + name);
                logger.LogError("Hello " + name);
                logger.LogWarn("Hello " + name);
                logger.LogDebug("Hello " + name);
            });

            return "Hello " + name;
        }

        [Route("GetEGMDetails")]
        [HttpPost]
        public async Task<IActionResult> Get([FromBody] Request<EGMDetailModel> request)
        {
            return await PerformOperation<EGMDetailModel>(request, async () =>
            {
                var response = await service.Get(request);
                return Ok(response);
            });
        }

        [Route("InsertEGMDetail")]
        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] Request<EGMDetailModel> request)
        {
            return await PerformOperation<EGMDetailModel>(request, async () =>
            {
                var response = await service.Insert(request);
                return Ok(response);
            });
        }

        [Route("UploadEGMDetails")]
        [HttpPost]
        public async Task<IActionResult> InsertAll([FromBody] Request<List<EGMDetailModel>> request)
        {
            return await PerformOperation<List<EGMDetailModel>>(request, async () =>
            {
                var response = await service.InsertAll(request);
                return Ok(response);
            });
        }

        [Route("UpdateEGMDetail")]
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] Request<EGMDetailModel> request)
        {
            return await PerformOperation<EGMDetailModel>(request, async () =>
            {
                var response = await service.Update(request);
                return Ok(response);
            });
        }

        [Route("DeleteEGMDetail")]
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