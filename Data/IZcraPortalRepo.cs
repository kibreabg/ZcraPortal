using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ZcraPortal.Data {
    public interface IZcraPortalRepo {
        IEnumerable<T> GetAll<T> () where T : class;
        T GetFirst<T> (Expression<Func<T, bool>> expression) where T : class;
    }
}