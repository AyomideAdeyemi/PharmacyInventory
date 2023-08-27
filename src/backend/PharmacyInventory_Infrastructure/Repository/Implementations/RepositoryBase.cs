using Microsoft.EntityFrameworkCore;
using PharmacyInventory_Infrastructure.Persistence;
using PharmacyInventory_Infrastructure.Repository.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyInventory_Infrastructure.Repository.Implementations
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected ApplicationDbContext RepositoryContext;
        public RepositoryBase(ApplicationDbContext repositoryContext)
        => RepositoryContext = repositoryContext;

        public IQueryable<T> FindAll(bool trackChanges) =>
        !trackChanges ?
        RepositoryContext.Set<T>()
        .AsNoTracking() :
        RepositoryContext.Set<T>();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression,
        bool trackChanges) =>
        !trackChanges ?
        RepositoryContext.Set<T>()
        .Where(expression)
        .AsNoTracking() :
        RepositoryContext.Set<T>()
        .Where(expression);


        public void Create(T entity) => RepositoryContext.Set<T>().Add(entity);
        public void Update(T entity) => RepositoryContext.Set<T>().Update(entity);
        public void Delete(T entity) => RepositoryContext.Set<T>().Remove(entity);
    }
}
