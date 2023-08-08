using CUni.Server.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CUni.Server.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected CUniContext _context;

        public RepositoryBase(CUniContext context)
        {
            _context = context;
        }

        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> condition)
        {
            return _context.Set<T>().Where(condition).AsNoTracking();
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsNoTracking();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

    }
}
