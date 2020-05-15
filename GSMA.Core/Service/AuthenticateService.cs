using AutoMapper;
using GSMA.Core.Interface;
using GSMA.DataProvider.Data;
using GSMA.DataProvider.UnitOfWork;
using GSMA.Logger;
using GSMA.Models;
using GSMA.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<UserModel> ValidateUserCredential(UserModel user)
        {
            if (user != null)
            {
                var validUser = await Task.Run(() => repository.GetQueryable().FirstOrDefault(x => x.Email.ToLower().Equals(user.Email.ToLower())));
                if (validUser != null)
                {
                    user = mapper.Map<UserModel>(validUser);
                }
                else
                { user = null; }
            }
            return user;
        }
    }
}
