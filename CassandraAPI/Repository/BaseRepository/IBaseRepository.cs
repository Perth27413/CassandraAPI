using CassandraAPI.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
namespace CassandraAPI.Repository
{
    public interface IBaseRepository
    {
        List<T> Gets<T>(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null) where T : class;
        IQueryable<T> GetsAsQueryAble<T>(Expression<Func<T, bool>> filter = null) where T : class;
        T GetItem<T>(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null) where T : class;
        T Create<T>(T model) where T : class;
        List<T> CreateList<T>(List<T> model) where T : class;
        List<T> GetPaginateData<T>(PageQuery pageQuery, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null) where T : class;
        int Count<T>(Expression<Func<T, bool>> predicate = null) where T : class;
        List<TResult> GetSelect<TSource, TResult>(Expression<Func<TSource, TResult>> selector, Expression<Func<TSource, bool>> predicate = null) where TSource : class;
        IQueryable<TResult> GetSelectAsQueryAble<TSource, TResult>(Expression<Func<TSource, TResult>> selector, Expression<Func<TSource, bool>> predicate = null) where TSource : class;
        IDbContextTransaction GetBeginTransaction();
        void Commit(IDbContextTransaction dbContextTransaction);
        void RollBack(IDbContextTransaction dbContextTransaction);
        void SaveChanges();
        List<T> UpdateRange<T>(List<T> entity) where T : class;
        T Update<T>(T entity) where T : class;
        void DisposeTransaction(IDbContextTransaction dbContextTransaction);
        List<T> GetInclude<T>(PageQuery pageQuery, Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "", bool isNoTracking = true) where T : class;
        T GetItemInclude<T>(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "", bool isNoTracking = true) where T : class;
        T Delete<T>(T model) where T : class;
        List<T> DeleteRange<T>(List<T> model) where T : class;
        List<T> GetItems<T>(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null) where T : class;
    }
}