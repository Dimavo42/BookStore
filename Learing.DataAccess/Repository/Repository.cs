using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LearningApp.DataAccess.Data;
using LearningApp.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LearningApp.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplictionDBContex _db;
        internal DbSet<T> dbSet;

        public Repository(ApplictionDBContex dBContex)
        {
            _db = dBContex;
            dbSet = _db.Set<T>();
            _db.Products.Include(query => query.Category);
            _db.SaveChanges();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
        //Category,CategoryID
        public IEnumerable<T> GetAll(string? includeProps = null)
        {
            IQueryable<T> query = dbSet;
            if (!string.IsNullOrEmpty(includeProps)) 
            { 
                foreach (var prop in includeProps
                    .Split(new char[] { ','},
                    StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(prop);
                }
            }
            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProps = null)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            if (!string.IsNullOrEmpty(includeProps))
            {
                foreach (var prop in includeProps
                    .Split(new char[] { ',' },
                    StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(prop);
                }
            }
            return query.FirstOrDefault();
        }

    }
}
