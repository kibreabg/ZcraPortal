using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ZcraPortal.Model;

namespace ZcraPortal.Data {
    public class MySqlZcraPortalRepo : IZcraPortalRepo
    {
        private readonly zhcraContext _context;

        public MySqlZcraPortalRepo(zhcraContext context)
        {
            _context = context;
        }
        public IEnumerable<T> GetAll<T>() where T : class
        {
            return _context.Set<T>().ToList();
        }

        public T GetFirst<T>(Expression<Func<T, bool>> expression) where T : class
        {
            return _context.Set<T>().FirstOrDefault();
        }
    }
}