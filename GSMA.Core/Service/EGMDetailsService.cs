﻿using AutoMapper;
using GSMA.Core.Interface;
using GSMA.DataProvider.Data;
using GSMA.DataProvider.UnitOfWork;
using GSMA.Core.Utilities;
using GSMA.Logger;
using GSMA.Models;
using GSMA.Models.Request;
using GSMA.Models.Response;
using GSMA.Repository.Repository;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GSMA.Core.Service
{
    public class EGMDetailsService : BaseService, IService<EGMDetailModel>
    {
        public IRepository<Egmdetails> repository;
        public EGMDetailsService(ILoggerManager _logger, IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            logger = _logger;
            unitOfWork = _unitOfWork;
            mapper = _mapper;
            repository = unitOfWork.GetRepository<Egmdetails>();
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
                        response.AddInformationMessage(MessageConstant.DELETE_INFORMATION_NO_DATA_FOUND);
                    }
                }
                else
                {
                    response.AddErrorMessage(MessageConstant.GENERAL_INVALID_ARGUMENT);
                }
            }
            catch (Exception ex)
            {
                response.AddErrorMessage(ex.Message);
            }

            return response;
        }

        public async Task<Response<List<EGMDetailModel>>> Get(Request<EGMDetailModel> request)
        {
            Response<List<EGMDetailModel>> response = new Response<List<EGMDetailModel>>();
            try
            {
                if (request != null && request.Entity != null)
                {
                    var resultSet = await Task.Run(() => repository.GetQueryable().Where(x => (x.Egmid == request.Entity.Egmid || request.Entity.Egmid == 0) &&
                                    (String.IsNullOrEmpty(request.Entity.Egmname) || x.Egmname == request.Entity.Egmname) &&
                                    (String.IsNullOrEmpty(request.Entity.EgmserialNumber) || x.EgmserialNumber == request.Entity.EgmserialNumber) &&
                                    (x.PortNumber == request.Entity.PortNumber || request.Entity.PortNumber == 0)).ToList());
                    if (resultSet.Any())
                    {
                        response = new Response<List<EGMDetailModel>>();
                        response.ResultSet = resultSet.Select(mapper.Map<Egmdetails, EGMDetailModel>).ToList();
                    }
                    else
                    {
                        response.AddInformationMessage(MessageConstant.GET_INFORMATION_NO_MATCHING_RECORDS_FOUND);
                    }
                }
                else
                {
                    response.AddErrorMessage(MessageConstant.GENERAL_INVALID_ARGUMENT);
                }
            }
            catch (Exception ex)
            {
                response.AddErrorMessage(ex.Message);
            }
            return response;
        }

        public async Task<Response<EGMDetailModel>> Insert(Request<EGMDetailModel> request)
        {
            Response<EGMDetailModel> response = new Response<EGMDetailModel>();
            try
            {
                if (request != null && request.Entity != null)
                {
                    var doiExist = await Get(request);
                    if (doiExist != null && doiExist.ResultSet == null)
                    {
                        var insertMe = mapper.Map<Egmdetails>(request.Entity);
                        await Task.Run(() =>
                        {
                            repository.Insert(insertMe);
                            unitOfWork.Save();
                        });
                        response.ResultSet = mapper.Map<EGMDetailModel>(insertMe);
                    }
                    else
                    {
                        response.AddErrorMessage(MessageConstant.INSERT_ERROR_RECORD_EXIST);
                    }
                }
                else
                {
                    response.AddErrorMessage(MessageConstant.GENERAL_INVALID_ARGUMENT);
                }
            }
            catch (Exception ex)
            {
                response.AddErrorMessage(ex.Message);
            }

            return response;
        }

        public async Task<Response<List<EGMDetailModel>>> InsertAll(Request<List<EGMDetailModel>> request)
        {
            Response<List<EGMDetailModel>> response = new Response<List<EGMDetailModel>>();
            try
            {
                if (request != null && request.Entity != null && request.Entity.Any())
                {

                    var insertMe = request.Entity.Select(mapper.Map<EGMDetailModel,Egmdetails>).ToList();
                    await Task.Run(() =>
                    {
                        repository.InsertAll(insertMe);
                        unitOfWork.Save();
                    });
                    response.ResultSet = insertMe.Select(mapper.Map<Egmdetails,EGMDetailModel>).ToList();
                }
                else
                {
                    response.AddErrorMessage(MessageConstant.GENERAL_INVALID_ARGUMENT);
                }
            }
            catch (Exception ex)
            {
                response.AddErrorMessage(ex.Message);
            }

            return response;
        }

        public async Task<Response<EGMDetailModel>> Update(Request<EGMDetailModel> request)
        {
            Response<EGMDetailModel> response = new Response<EGMDetailModel>();
            try
            {
                if (request != null && request.Entity != null)
                {
                    var updateMe = await Task.Run(() => repository.GetByID(request.Entity.Id));
                    if (updateMe != null)
                    {
                        updateMe = mapper.Map<Egmdetails>(request.Entity);
                        await Task.Run(() =>
                        {
                            repository.Update(updateMe);
                            unitOfWork.Save();
                        });
                        response.ResultSet = mapper.Map<EGMDetailModel>(updateMe);
                    }
                    else
                    {
                        response.AddErrorMessage(MessageConstant.UPDATE_ERROR_NO_RECORD_EXIST);
                    }
                }
                else
                {
                    response.AddErrorMessage(MessageConstant.GENERAL_INVALID_ARGUMENT);
                }
            }
            catch (Exception ex)
            {
                response.AddErrorMessage(ex.Message);
            }

            return response;
        }
    }
}