using AutoMapper;
using GSMA.Core.Interface;
using GSMA.DataProvider.Data;
using GSMA.DataProvider.UnitOfWork;
using GSMA.Logger;
using GSMA.Models;
using GSMA.Models.Request;
using GSMA.Models.Response;
using GSMA.Repository.Repository;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GSMA.Core.Service
{
    public class AuthenticateService : BaseService, IAuthenticateService
    {
        public IRepository<User> repository;

        public AuthenticateService(ILoggerManager _logger, IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            logger = _logger;
            unitOfWork = _unitOfWork;
            mapper = _mapper;
            repository = unitOfWork.GetRepository<User>();
        }

        public async Task<Response<UserModel>> ValidateUserCredential(Request<UserModel> request)
        {
            var response = new Response<UserModel>();
            try
            {
                if (request != null && request.Entity != null)
                {
                    var validUser = await Task.Run(() => repository.GetQueryable().FirstOrDefault(x => x.UserName.ToLower().Equals(request.Entity.UserName.ToLower()) && x.Password.Equals(request.Entity.Password)));
                    if (validUser != null)
                    {
                        response.ResultSet = mapper.Map<UserModel>(validUser);
                    }
                    else
                    { response.ResultSet = null; }
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
