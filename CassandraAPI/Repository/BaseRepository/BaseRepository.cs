using CassandraAPI.Data;
using CassandraAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CassandraAPI.Repository
{
    public class BaseRepository : IBaseRepository
    {
        private readonly MainContext _context;

        public BaseRepository(MainContext context)
        {
            this._context = context;
        }
        public List<T> Gets<T>(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null) where T : class
        {
            IQueryable<T> query = this._context.Set<T>();
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (filter != null) { query = query.Where(filter); }
            return query.ToList();
        }

        public IQueryable<T> GetsAsQueryAble<T>(Expression<Func<T, bool>> filter = null) where T : class
        {
            IQueryable<T> query = this._context.Set<T>();
            if (filter != null) { query = query.Where(filter); }
            return query;
        }
        public T GetItem<T>(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null) where T : class
        {
            T result = null;
            IQueryable<T> query = this._context.Set<T>();
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (filter != null)
            {
                result = query.Where(filter).FirstOrDefault();
            }
            if (filter == null)
            {
                result = query.FirstOrDefault();
            }
            return result;
        }
        public T Create<T>(T model) where T : class
        {
            this._context.Add(model);
            this._context.SaveChanges();
            return model;
        }
        public List<T> CreateList<T>(List<T> model) where T : class
        {
            this._context.AddRange(model);
            this._context.SaveChanges();
            return model;
        }
        public List<T> GetPaginateData<T>(PageQuery pageQuery, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null) where T : class
        {
            IQueryable<T> query = this._context.Set<T>();
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (predicate != null)
            {
                query = query.Where(predicate).Skip((pageQuery.pageNumber - 1) * pageQuery.pageSize).Take(pageQuery.pageSize);
            }
            else
            {
                query = query.Skip((pageQuery.pageNumber - 1) * pageQuery.pageSize).Take(pageQuery.pageSize);
            }
            return query.ToList();
        }
        public int Count<T>(Expression<Func<T, bool>> predicate = null) where T : class
        {
            int result = 0;
            IQueryable<T> query = this._context.Set<T>();
            if (predicate != null)
            {
                result = query.Where(predicate).Count();
            }
            else
            {
                result = query.Count();
            }
            return result;
        }

        public List<TResult> GetSelect<TSource, TResult>(Expression<Func<TSource, TResult>> selector, Expression<Func<TSource, bool>> predicate = null) where TSource : class
        {
            List<TResult> results;
            IQueryable<TSource> query = this._context.Set<TSource>();
            if (predicate != null)
            {
                results = query.Where(predicate).Select(selector).ToList();
            }
            else
            {
                results = query.Select(selector).ToList();
            }
            return results;
        }

        public IQueryable<TResult> GetSelectAsQueryAble<TSource, TResult>(Expression<Func<TSource, TResult>> selector, Expression<Func<TSource, bool>> predicate = null) where TSource : class
        {
            IQueryable<TResult> results;
            IQueryable<TSource> query = this._context.Set<TSource>();
            if (predicate != null)
            {
                results = query.Where(predicate).Select(selector);
            }
            else
            {
                results = query.Select(selector);
            }
            return results;
        }
        public IDbContextTransaction GetBeginTransaction()
        {
            return this._context.Database.BeginTransaction();
        }
        public void Commit(IDbContextTransaction dbContextTransaction)
        {
            dbContextTransaction.Commit();
        }
        public void RollBack(IDbContextTransaction dbContextTransaction)
        {
            dbContextTransaction.Rollback();
        }
        public void SaveChanges()
        {
            this._context.SaveChanges();
        }
        public T Update<T>(T entity) where T : class
        {
            this._context.Entry(entity).State = EntityState.Modified;
            this._context.SaveChanges();
            return entity;
        }

        public List<T> UpdateRange<T>(List<T> entity) where T : class
        {
            this._context.UpdateRange(entity);
            this._context.SaveChanges();
            return entity;
        }
        public void DisposeTransaction(IDbContextTransaction dbContextTransaction)
        {
            dbContextTransaction.Dispose();
        }
        public List<T> GetInclude<T>(PageQuery pageQuery, Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "", bool isNoTracking = true) where T : class
        {
            IQueryable<T> query = this._context.Set<T>();
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (includeProperties != null)
                foreach (string includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            if (isNoTracking)
                query = query.AsNoTracking();
            if (filter != null && pageQuery != null)
            {
                return query.Where(filter).Skip((pageQuery.pageNumber - 1) * pageQuery.pageSize).Take(pageQuery.pageSize).ToList();
            }
            else if (pageQuery == null && filter == null)
            {
                return query.ToList();
            }
            else if (pageQuery == null)
            {
                return query.Where(filter).ToList();
            }
            else
            {
                return query.Skip((pageQuery.pageNumber - 1) * pageQuery.pageSize).Take(pageQuery.pageSize).ToList();
            }
        }
        public T GetItemInclude<T>(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "", bool isNoTracking = true) where T : class
        {
            T result = null;
            IQueryable<T> query = this._context.Set<T>();
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (includeProperties != null)
                foreach (string includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            if (isNoTracking)
                query = query.AsNoTracking();
            if (filter != null)
            {
                result = query.Where(filter).FirstOrDefault();
            }
            return result;
        }
        public T Delete<T>(T model) where T : class
        {
            this._context.Remove(model);
            this._context.SaveChanges();
            return model;
        }
        public List<T> DeleteRange<T>(List<T> model) where T : class
        {
            this._context.RemoveRange(model);
            this._context.SaveChanges();
            return model;
        }
        public List<T> GetItems<T>(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null) where T : class
        {
            IQueryable<T> query = this._context.Set<T>();
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (filter != null) { query = query.Where(filter); }
            return query.ToList();
        }
    }
}