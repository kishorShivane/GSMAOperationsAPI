using GSMA.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSMA.DataProvider.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : class;
        void Save();
    }
}
