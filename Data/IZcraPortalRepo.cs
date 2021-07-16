using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ZcraPortal.Data
{
    public interface IZcraPortalRepo
    {
        bool SaveChanges();
        IEnumerable<T> GetAll<T>() where T : class;
        T GetFirst<T>(Expression<Func<T, bool>> expression) where T : class;
        IEnumerable<T> GetSomeById<T>(Expression<Func<T, bool>> expression) where T : class;
        void Create<T>(T item) where T : class;
        void Update<T>(T item) where T : class;
        void Delete<T>(T item) where T : class;
    }
}