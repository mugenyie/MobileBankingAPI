using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileBanking.Data
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Query();

        T GetById(int Id);

        void Add(T entity);

        void AddRange(IEnumerable<T> entity);

        IDbContextTransaction BeginTransaction();

        void SaveChanges();

        void Update(T entity);

        void Remove(T entity);
    }
}
