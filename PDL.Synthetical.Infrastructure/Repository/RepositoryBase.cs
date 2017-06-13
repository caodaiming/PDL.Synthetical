using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DapperExtensions;
using System.Linq.Expressions;

namespace PDL.Synthetical.Infrastructure.Repository
{
    public abstract class RepositoryBase<T> where T : class
    {
        private IDbConnection dbConnection;

        protected IConnectionFactory ConnectionFactory { get; private set; }

        protected RepositoryBase(IConnectionFactory connectionFactory)
        {
            ConnectionFactory = connectionFactory;
        }

        protected IDbConnection DbConnection
        {
            get { return dbConnection ?? (dbConnection = ConnectionFactory.DbConnection); }
        }

        public virtual void Add(T entity)
        {
            DbConnection.Insert(entity);
        }

        public virtual void Update(T entity)
        {
            DbConnection.Update(entity);
        }

        public virtual void Delete(T entity)
        {
            DbConnection.Delete(entity);
        }

        public async Task<T> GetByIdAsync(string Id)
        {
            return await DbConnection.GetAsync<T>(Id);
        }

        public virtual T GetById(long Id)
        {
            return DbConnection.Get<T>(Id);
        }

        public virtual T GetById(int Id)
        {
            return DbConnection.Get<T>(Id);
        }

        public virtual T GetById(string Id)
        {
            return DbConnection.Get<T>(Id);
        }

        public virtual async Task<T> GetByIdAsync(long Id)
        {
            return await DbConnection.GetAsync<T>(Id);
        }

        public virtual async Task<T> GetByIdAsync(int Id)
        {
            return await DbConnection.GetAsync<T>(Id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return DbConnection.GetList<T>();
        }
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await DbConnection.GetListAsync<T>();
        }
        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return DbConnection.GetList<T>().AsQueryable().Where(where);
        }

        public virtual async Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> where)
        {
            return await DbConnection.GetListAsync<T>();
        }

        public virtual IEnumerable<T> GetPageList(int pageIndex, int pageSize, out long rowsCount, object predicate = null,
            IList<ISort> sort = null, bool buffer = true)
        {
            if (sort == null) sort = new List<ISort>();
            rowsCount = DbConnection.Count<T>(predicate);
            return DbConnection.GetPage<T>(predicate, sort, pageIndex, pageSize, null, null, buffer);
        }


        /// <summary>
        /// insert batch data.
        /// </summary>
        /// <param name="entityList">isnert entity.</param>
        /// <returns>true/false</returns>
        public virtual bool AddBatch(IEnumerable<T> entityList)
        {
            bool isSuccess = false;
            foreach (var item in entityList)
            {
                DbConnection.Insert<T>(item);
            }
            isSuccess = true;
            return isSuccess;
        }    

    }
}
