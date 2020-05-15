using GSMA.Models.Request;
using GSMA.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GSMA.Core.Interface
{
    public interface IService<T>
    {
        Task<Response<T>> Insert(Request<T> entity);
        Task<Response<List<T>>> InsertAll(Request<List<T>> entity);
        Task<Response<T>> Update(Request<T> entity);
        Task<Response<bool>> Delete(int id);
        Task<Response<List<T>>> Get(Request<T> entity);
    }
}
