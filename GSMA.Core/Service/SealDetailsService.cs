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
    public class SealDetailsService : BaseService, IService<SealDetailModel>
    {
        public IRepository<SealDetails> repository;

        public SealDetailsService(ILoggerManager _logger, IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            logger = _logger;
            unitOfWork = _unitOfWork;
            mapper = _mapper;
            repository = unitOfWork.GetRepository<SealDetails>();
        }

        public async Task<Response<bool>> Delete(int id)
        {
            Response<bool> response = new Response<bool>();
            try
            {
                if (id > 0)
                {
                    var deleteMe = await Task.Run(() => repository.Get(x => x.Id == id).FirstOrDefault());
                    if (deleteMe != null)
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

        public async Task<Response<List<SealDetailModel>>> Get(Request<SealDetailModel> request)
        {
            Response<List<SealDetailModel>> response = new Response<List<SealDetailModel>>();
            try
            {
                if (request != null && request.Entity != null)
                {
                    var resultSet = await Task.Run(() => repository.GetQueryable().Where(x => (x.Id == request.Entity.Id || request.Entity.Id == 0) &&
                                    (String.IsNullOrEmpty(request.Entity.SerialNumber) || x.SerialNumber == request.Entity.SerialNumber)).ToList());
                    if (resultSet.Any())
                    {
                        response = new Response<List<SealDetailModel>>();
                        response.ResultSet = resultSet.Select(mapper.Map<SealDetails, SealDetailModel>).ToList();
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

        public async Task<Response<SealDetailModel>> Insert(Request<SealDetailModel> request)
        {
            Response<SealDetailModel> response = new Response<SealDetailModel>();
            try
            {
                if (request != null && request.Entity != null)
                {
                    var doiExist = await Get(request);
                    if (doiExist != null && doiExist.ResultSet == null)
                    {
                        var insertMe = mapper.Map<SealDetails>(request.Entity);
                        await Task.Run(() =>
                        {
                            repository.Insert(insertMe);
                            unitOfWork.Save();
                        });
                        response.ResultSet = mapper.Map<SealDetailModel>(insertMe);
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

        public async Task<Response<List<SealDetailModel>>> InsertAll(Request<List<SealDetailModel>> request)
        {
            Response<List<SealDetailModel>> response = new Response<List<SealDetailModel>>();
            try
            {
                if (request != null && request.Entity != null && request.Entity.Any())
                {
                    var insertMe = request.Entity.Select(mapper.Map<SealDetailModel, SealDetails>).ToList();
                    await Task.Run(() =>
                    {
                        repository.InsertAll(insertMe);
                        unitOfWork.Save();
                    });
                    response.ResultSet = insertMe.Select(mapper.Map<SealDetails, SealDetailModel>).ToList();
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

        public async Task<Response<SealDetailModel>> Update(Request<SealDetailModel> request)
        {
            Response<SealDetailModel> response = new Response<SealDetailModel>();
            try
            {
                if (request != null && request.Entity != null)
                {
                    var updateMe = await Task.Run(() => repository.GetByID(request.Entity.Id));
                    if (updateMe != null)
                    {
                        updateMe.SerialNumber = request.Entity.SerialNumber;

                        await Task.Run(() =>
                        {
                            repository.Update(updateMe);
                            unitOfWork.Save();
                        });
                        response.ResultSet = mapper.Map<SealDetailModel>(updateMe);
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
