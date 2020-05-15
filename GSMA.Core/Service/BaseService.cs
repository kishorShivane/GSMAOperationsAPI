using AutoMapper;
using GSMA.DataProvider.UnitOfWork;
using GSMA.Logger;

namespace GSMA.Core.Service
{
    public class BaseService
    {
        public ILoggerManager logger;
        public IUnitOfWork unitOfWork;
        public IMapper mapper;
        public BaseService()
        { }
    }
}