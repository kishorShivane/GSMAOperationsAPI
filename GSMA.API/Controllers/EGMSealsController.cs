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
    public class EGMSealsController : GSMABaseController
    {
        IService<EGMSealModel> service;
        public EGMSealsController(ILoggerManager _logger, IService<EGMSealModel> _service)
        {
            logger = _logger;
            service = _service;
        }

        [Route("GetEGMSeals")]
        [HttpPost]
        public async Task<IActionResult> Get([FromBody] Request<EGMSealModel> request)
        {
            return await PerformOperation<EGMSealModel>(request, async () =>
            {
                var response = await service.Get(request);
                return Ok(response);
            });
        }

        [Route("InsertEGMSeal")]
        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] Request<EGMSealModel> request)
        {
            return await PerformOperation<EGMSealModel>(request, async () =>
            {
                var response = await service.Insert(request);
                return Ok(response);
            });
        }

        [Route("UploadEGMSeals")]
        [HttpPost]
        public async Task<IActionResult> InsertAll([FromBody] Request<List<EGMSealModel>> request)
        {
            return await PerformOperation<List<EGMSealModel>>(request, async () =>
            {
                var response = await service.InsertAll(request);
                return Ok(response);
            });
        }

        [Route("UpdateEGMSeal")]
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] Request<EGMSealModel> request)
        {
            return await PerformOperation<EGMSealModel>(request, async () =>
            {
                var response = await service.Update(request);
                return Ok(response);
            });
        }

        [Route("DeleteEGMSeal")]
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