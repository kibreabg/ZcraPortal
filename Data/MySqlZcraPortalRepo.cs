using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ZcraPortal.Model;

namespace ZcraPortal.Data {
    public class MySqlZcraPortalRepo : IZcraPortalRepo {
        private readonly zhcraContext _context;

        public MySqlZcraPortalRepo (zhcraContext context) {
            _context = context;
        }

        public void Create<T> (T item) where T : class {
            if (item == null) {
                throw new ArgumentNullException (nameof (item));
            }

            _context.Set<T> ().Add (item);
        }

        public IEnumerable<T> GetAll<T> () where T : class {
            return _context.Set<T> ().ToList ();
        }

        public IEnumerable<T> GetSomeById<T>(Expression<Func<T, bool>> expression) where T : class
        {
            return _context.Set<T>().Where<T>(expression).ToList();
        }

        public T GetFirst<T> (Expression<Func<T, bool>> expression) where T : class {
            return _context.Set<T> ().FirstOrDefault (expression);
        }

        public void Update<T> (T item) where T : class {

        }

        public bool SaveChanges () {
            return (_context.SaveChanges () >= 0);
        }

        public void Delete<T> (T item) where T : class {
            if (item == null) {
                throw new ArgumentNullException (nameof (item));
            }
            _context.Set<T> ().Remove (item);
        }
    }
}