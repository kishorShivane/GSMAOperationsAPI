using GSMA.DataProvider.Data;
using GSMA.Repository.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GSMA.DataProvider.UnitOfWork
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private GSMAContext context;

        private Hashtable repositories = new Hashtable();
        public UnitOfWork()
        {
            var connString = "";
            if (string.IsNullOrEmpty(connString))
                context = new GSMAContext();
            else
                context = new GSMAContext(connString);
        }
        public IRepository<T> GetRepository<T>() where T : class
        {
            if (!repositories.Contains(typeof(T)))
            {
                repositories.Add(typeof(T), new Repository<T>(context));
            }
            return (IRepository<T>)repositories[typeof(T)];
        }

        public void Save()
        {
            context.SaveChanges();
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
