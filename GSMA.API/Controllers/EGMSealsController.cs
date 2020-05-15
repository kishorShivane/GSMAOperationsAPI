﻿using GSMA.API.Filters;
using GSMA.Logger;
using GSMA.Models;
using GSMA.Models.Request;
using GSMA.Models.Response;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using GSMA.Core.Interface;
using System;

namespace GSMA.API.Controllers
{
    [Route("api/[controller]")]
    [UsageLog]
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