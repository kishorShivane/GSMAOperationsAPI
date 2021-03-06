﻿using GSMA.API.Filters;
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
    public class SealDetailsController : GSMABaseController
    {
        IService<SealDetailModel> service;
        public SealDetailsController(ILoggerManager _logger, IService<SealDetailModel> _service)
        {
            logger = _logger;
            service = _service;
        }

        [Route("GetSealDetails")]
        [HttpPost]
        public async Task<IActionResult> Get([FromBody] Request<SealDetailModel> request)
        {
            return await PerformOperation<SealDetailModel>(request, async () =>
            {
                var response = await service.Get(request);
                return Ok(response);
            });
        }

        [Route("InsertSealDetail")]
        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] Request<SealDetailModel> request)
        {
            return await PerformOperation<SealDetailModel>(request, async () =>
            {
                var response = await service.Insert(request);
                return Ok(response);
            });
        }

        [Route("UploadSealDetails")]
        [HttpPost]
        public async Task<IActionResult> InsertAll([FromBody] Request<List<SealDetailModel>> request)
        {
            return await PerformOperation<List<SealDetailModel>>(request, async () =>
            {
                var response = await service.InsertAll(request);
                return Ok(response);
            });
        }

        [Route("UpdateSealDetail")]
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] Request<SealDetailModel> request)
        {
            return await PerformOperation<SealDetailModel>(request, async () =>
            {
                var response = await service.Update(request);
                return Ok(response);
            });
        }

        [Route("DeleteSealDetail")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            return await PerformOperation(id, async () =>
            {
                var response = await service.Delete(id);
                return Ok(response);
            });
        }
    }
}