using AutoMapper;
using GSMA.Core.Interface;
using GSMA.Core.Utilities;
using GSMA.DataProvider.Data;
using GSMA.DataProvider.UnitOfWork;
using GSMA.Logger;
using GSMA.Models;
using GSMA.Models.Request;
using GSMA.Models.Response;
using GSMA.Repository.Repository;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GSMA.Core.Service
{
    public class EGMSealsService : BaseService, IService<EGMSealModel>
    {
        public IRepository<Egmseals> repository;

        public EGMSealsService(ILoggerManager _logger, IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            logger = _logger;
            unitOfWork = _unitOfWork;
            mapper = _mapper;
            repository = unitOfWork.GetRepository<Egmseals>();
        }

        public async Task<Response<bool>> Delete(int id)
        {
            Response<bool> response = new Response<bool>();
            try
            {
                if (id > 0)
                {
                    var deleteMe = await Task.Run(() => repository.Get(x => x.Id == id).ToList());
                    if (deleteMe != null && deleteMe.Any())
                    {
                        await Task.Run(() => repository.Delete(deleteMe));
                    }
                    else
                    {
                        response.AddInformationMessage(MessageConstant.DELETE_INFORMATION_NO_DATA_FOUND+": ID = " + id);
                        logger.LogInfo(MessageConstant.DELETE_INFORMATION_NO_DATA_FOUND+": ID = " + id);
                    }
                }
                else
                {
                    response.AddErrorMessage(MessageConstant.GENERAL_INVALID_ARGUMENT);
                    logger.LogInfo(MessageConstant.GENERAL_INVALID_ARGUMENT);
                }
            }
            catch (Exception ex)
            {
                string message = JsonConvert.SerializeObject(ex).ToString();
                response.AddErrorMessage(ex.Message);
                logger.LogError(message);
            }

            return response;
        }

        public async Task<Response<List<EGMSealModel>>> Get(Request<EGMSealModel> request)
        {
            Response<List<EGMSealModel>> response = new Response<List<EGMSealModel>>();
            try
            {
                if (request != null && request.Entity != null)
                {
                    var resultSet = await Task.Run(() => repository.GetQueryable().Where(x => (x.Egmid == request.Entity.Egmid || request.Entity.Egmid == 0) &&
                                    (x.SealId == request.Entity.SealId || request.Entity.SealId == 0) &&
                                    (request.Entity.Captureddatetime == null || x.Captureddatetime == request.Entity.Captureddatetime) &&
                                    (request.Entity.JobCompleateDateTime == null || x.JobCompleateDateTime == request.Entity.JobCompleateDateTime) &&
                                    (x.AssaignedUserId == request.Entity.AssaignedUserId || request.Entity.AssaignedUserId == 0) &&
                                    (x.CapturedUserId == request.Entity.CapturedUserId || request.Entity.CapturedUserId == 0)).ToList());
                    if (resultSet.Any())
                    {
                        response = new Response<List<EGMSealModel>>() { ResultSet = resultSet.Select(mapper.Map<Egmseals, EGMSealModel>).ToList() };
                        logger.LogInfo("Found: " + response.ResultSet.Count() + " records");
                    }
                    else
                    {
                        var requestString = JsonConvert.SerializeObject(request);
                        response.AddInformationMessage(MessageConstant.GET_INFORMATION_NO_MATCHING_RECORDS_FOUND);
                        logger.LogInfo(MessageConstant.GET_INFORMATION_NO_MATCHING_RECORDS_FOUND + " Request: " + requestString);
                    }
                }
                else
                {
                    response.AddErrorMessage(MessageConstant.GENERAL_INVALID_ARGUMENT);
                    logger.LogInfo(MessageConstant.GENERAL_INVALID_ARGUMENT);
                }
            }
            catch (Exception ex)
            {
                string message = JsonConvert.SerializeObject(ex).ToString();
                response.AddErrorMessage(ex.Message);
                logger.LogError(message);
            }
            return response;
        }

        public async Task<Response<EGMSealModel>> Insert(Request<EGMSealModel> request)
        {
            Response<EGMSealModel> response = new Response<EGMSealModel>();
            try
            {
                if (request != null && request.Entity != null)
                {
                    var doiExist = await Get(request);
                    if (doiExist != null && doiExist.ResultSet == null)
                    {
                        var insertMe = mapper.Map<Egmseals>(request.Entity);
                        await Task.Run(() =>
                        {
                            repository.Insert(insertMe);
                            unitOfWork.Save();
                        });
                        response.ResultSet = mapper.Map<EGMSealModel>(insertMe);
                    }
                    else
                    {
                        var requestString = JsonConvert.SerializeObject(request);
                        response.AddErrorMessage(MessageConstant.INSERT_ERROR_RECORD_EXIST);
                        logger.LogInfo(MessageConstant.INSERT_ERROR_RECORD_EXIST + " Request: " + requestString);
                    }
                }
                else
                {
                    response.AddErrorMessage(MessageConstant.GENERAL_INVALID_ARGUMENT);
                    logger.LogInfo(MessageConstant.GENERAL_INVALID_ARGUMENT);
                }
            }
            catch (Exception ex)
            {
                string message = JsonConvert.SerializeObject(ex).ToString();
                response.AddErrorMessage(ex.Message);
                logger.LogError(message);
            }

            return response;
        }

        public async Task<Response<List<EGMSealModel>>> InsertAll(Request<List<EGMSealModel>> request)
        {
            Response<List<EGMSealModel>> response = new Response<List<EGMSealModel>>();
            try
            {
                if (request != null && request.Entity != null && request.Entity.Any())
                {
                    var insertMe = request.Entity.Select(mapper.Map<EGMSealModel,Egmseals>);
                    await Task.Run(() =>
                    {
                        repository.InsertAll(insertMe);
                        unitOfWork.Save();
                    });
                    response.ResultSet = insertMe.Select(mapper.Map<Egmseals, EGMSealModel>).ToList();
                }
                else
                {
                    response.AddErrorMessage(MessageConstant.GENERAL_INVALID_ARGUMENT);
                    logger.LogInfo(MessageConstant.GENERAL_INVALID_ARGUMENT);
                }
            }
            catch (Exception ex)
            {
                string message = JsonConvert.SerializeObject(ex).ToString();
                response.AddErrorMessage(ex.Message);
                logger.LogError(message);
            }

            return response;
        }

        public async Task<Response<EGMSealModel>> Update(Request<EGMSealModel> request)
        {
            Response<EGMSealModel> response = new Response<EGMSealModel>();
            try
            {
                if (request != null && request.Entity != null)
                {
                    var updateMe = await Task.Run(() => repository.GetByID(request.Entity.Id));
                    if (updateMe != null)
                    {
                        updateMe = mapper.Map<Egmseals>(request.Entity);
                        await Task.Run(() =>
                        {
                            repository.Update(updateMe);
                            unitOfWork.Save();
                        });
                        response.ResultSet = mapper.Map<EGMSealModel>(updateMe);
                    }
                    else
                    {
                        var requestString = JsonConvert.SerializeObject(request);
                        response.AddErrorMessage(MessageConstant.UPDATE_ERROR_NO_RECORD_EXIST);
                        logger.LogInfo(MessageConstant.UPDATE_ERROR_NO_RECORD_EXIST + " Request: " + requestString);
                    }
                }
                else
                {
                    response.AddErrorMessage(MessageConstant.GENERAL_INVALID_ARGUMENT);
                    logger.LogInfo(MessageConstant.GENERAL_INVALID_ARGUMENT);
                }
            }
            catch (Exception ex)
            {
                string message = JsonConvert.SerializeObject(ex).ToString();
                response.AddErrorMessage(ex.Message);
                logger.LogError(message);
            }

            return response;
        }
    }
}
