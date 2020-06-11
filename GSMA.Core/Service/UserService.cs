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
    public class UserService : BaseService, IService<UserModel>
    {
        public IRepository<User> repository;

        public UserService(ILoggerManager _logger, IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            logger = _logger;
            unitOfWork = _unitOfWork;
            mapper = _mapper;
            repository = unitOfWork.GetRepository<User>();
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
                        response.AddInformationMessage(MessageConstant.DELETE_INFORMATION_NO_DATA_FOUND + ": ID = " + id);
                        logger.LogInfo(MessageConstant.DELETE_INFORMATION_NO_DATA_FOUND + ": ID = " + id);
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

        public async Task<Response<List<UserModel>>> Get(Request<UserModel> request)
        {
            Response<List<UserModel>> response = new Response<List<UserModel>>();
            try
            {
                if (request != null && request.Entity != null)
                {
                    var resultSet = await Task.Run(() => repository.GetQueryable().Where(x => (x.Id == request.Entity.Id || request.Entity.Id == 0) &&
                                    (x.UserTypeId == request.Entity.UserTypeId || request.Entity.UserTypeId == 0) &&
                                    (String.IsNullOrEmpty(request.Entity.FirstName) || x.FirstName == request.Entity.FirstName) &&
                                    (String.IsNullOrEmpty(request.Entity.LastName) || x.LastName == request.Entity.LastName) &&
                                    (String.IsNullOrEmpty(request.Entity.UserName) || x.UserName == request.Entity.UserName) &&
                                    (String.IsNullOrEmpty(request.Entity.Email) || x.Email == request.Entity.Email) &&
                                    (request.Entity.CapturedDateTime == null || x.CapturedDateTime == request.Entity.CapturedDateTime)).ToList());
                    if (resultSet.Any())
                    {
                        response = new Response<List<UserModel>>();
                        response.ResultSet = resultSet.Select(mapper.Map<User,UserModel>).ToList();
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

        public async Task<Response<UserModel>> Insert(Request<UserModel> request)
        {
            Response<UserModel> response = new Response<UserModel>();
            try
            {
                if (request != null && request.Entity != null)
                {
                    var doiExist = await Get(request);
                    if (doiExist != null && doiExist.ResultSet == null)
                    {
                        var insertMe = mapper.Map<User>(request.Entity);
                        await Task.Run(() =>
                        {
                            repository.Insert(insertMe);
                            unitOfWork.Save();
                        });
                        response.ResultSet = mapper.Map<UserModel>(insertMe);
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

        public async Task<Response<List<UserModel>>> InsertAll(Request<List<UserModel>> request)
        {
            Response<List<UserModel>> response = new Response<List<UserModel>>();
            try
            {
                if (request != null && request.Entity != null && request.Entity.Any())
                {
                    var insertMe = request.Entity.Select(mapper.Map<UserModel, User>).ToList();
                    await Task.Run(() =>
                    {
                        repository.InsertAll(insertMe);
                        unitOfWork.Save();
                    });
                    response.ResultSet = insertMe.Select(mapper.Map<User, UserModel>).ToList();
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

        public async Task<Response<UserModel>> Update(Request<UserModel> request)
        {
            Response<UserModel> response = new Response<UserModel>();
            try
            {
                if (request != null && request.Entity != null)
                {
                    var updateMe = await Task.Run(() => repository.GetByID(request.Entity.Id));
                    if (updateMe != null)
                    {
                        updateMe = mapper.Map<User>(request.Entity);
                        await Task.Run(() =>
                        {
                            repository.Update(updateMe);
                            unitOfWork.Save();
                        });
                        response.ResultSet = mapper.Map<UserModel>(updateMe);
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
