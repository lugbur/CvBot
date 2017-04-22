using CVBot.BussinessLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CVBot.DataAccess.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : Entity
    {
        #region Fields

        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Constructor

        /// <summary>
        /// Create a new instance of repository
        /// </summary>
        /// <param name="unitOfWork">Associated Unit Of Work</param>
        public GenericRepository(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
                throw new ArgumentNullException("unitOfWork");

            _unitOfWork = unitOfWork;
        }

        #endregion

        #region IRepository

        /// <summary>
        /// Get the unit of work in this repository
        /// </summary>
        public IUnitOfWork UnitOfWork
        {
            get { return _unitOfWork; }
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="item">Item</param>
        public virtual void Add(TEntity item)
        {

            if (item != null)
            {
                try
                {
                    _unitOfWork.GetDbSet<TEntity>().Add(item);
                }
                catch (Exception ex)
                {
                    //TODO:ExceptionFactory
                }
            }
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="item">Item</param>
        public virtual void Modify(TEntity item)
        {

            if (item != null)
            {
                try
                {
                    var itemInContext = _unitOfWork.GetExistingEntityInContextOrNull(item);
                    if (itemInContext != null)
                        Merge(itemInContext, item);
                    else
                        _unitOfWork.SetEntryState(item, EntityState.Modified);
                }
                catch (Exception ex)
                {
                    //TODO:ExceptionFactory
                }
            }
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="item">Item</param>
        public virtual void Remove(TEntity item)
        {

            if (item != null)
            {
                try
                {
                    var itemInContext = _unitOfWork.GetExistingEntityInContextOrNull(item);
                    if (itemInContext == null)
                        _unitOfWork.SetEntryState(item, EntityState.Unchanged);

                    _unitOfWork.GetDbSet<TEntity>().Remove(itemInContext ?? item);
                }
                catch (Exception ex)
                {
                    //TODO:ExceptionFactory
                }
            }
        }

        /// <summary>
        /// Merge
        /// </summary>
        /// <param name="persisted">Persisted item</param>
        /// <param name="current">Current item</param>
        public virtual void Merge(TEntity persisted, TEntity current)
        {

            try
            {
                _unitOfWork.ApplyCurrentValues(persisted, current);
            }
            catch (Exception ex)
            {
                //TODO:ExceptionFactory
            }
        }

        /// <summary>
        /// Select
        /// </summary>
        /// <param name="keyValues">Entity key values</param>
        /// <returns>Entity</returns>
        public virtual TEntity Get(List<object> keyValues)
        {
            TEntity item = null;

            try
            {
                item = _unitOfWork.GetDbSet<TEntity>().Find(keyValues.ToArray());
            }
            catch (Exception ex)
            {
                //TODO:ExceptionFactory
            }

            return item;
        }

        /// <summary>
        /// Select async
        /// </summary>
        /// <param name="keyValues">Entity key values</param>
        /// <returns>TEntity (task)</returns>
        public virtual async Task<TEntity> GetAsync(List<object> keyValues)
        {
            TEntity item = null;

            try
            {
                item = await _unitOfWork.GetDbSet<TEntity>().FindAsync(keyValues.ToArray());
            }
            catch (Exception ex)
            {
                //TODO:ExceptionFactory
            }

            return item;
        }

        /// <summary>
        /// Select all
        /// </summary>
        /// <param name="includes">Inners</param> 
        /// <returns>List of results</returns>
        public virtual List<TEntity> GetAll(List<string> includes = null)
        {
            List<TEntity> items = null;

            try
            {
                items = _unitOfWork.GetQueryable<TEntity>(includes).ToList();
            }
            catch (Exception ex)
            {
                //TODO:ExceptionFactory
            }

            return items;
        }

        /// <summary>
        /// Select all async
        /// </summary>
        /// <param name="includes">Inners</param>
        /// <returns>List of results (task)</returns>
        public virtual async Task<List<TEntity>> GetAllAsync(List<string> includes = null)
        {
            List<TEntity> items = null;

            try
            {
                items = await _unitOfWork.GetQueryable<TEntity>(includes).ToListAsync();
            }
            catch (Exception ex)
            {
                //TODO:ExceptionFactory
            }

            return items;
        }

        /// <summary>
        /// Find items
        /// </summary>
        /// <param name="filter">Expression filter</param>
        /// <param name="includes">Inners</param>
        /// <returns>List of results</returns>
        public virtual List<TEntity> AllMatching(Expression<Func<TEntity, bool>> filter, List<string> includes = null)
        {
            List<TEntity> items = null;

            try
            {
                items = _unitOfWork.GetQueryable(includes, filter).ToList();
            }
            catch (Exception ex)
            {
                //TODO:ExceptionFactory
            }

            return items;
        }

        /// <summary>
        /// Find items async
        /// </summary>
        /// <param name="filter">Expression filter</param>
        /// <param name="includes">Inners</param>
        /// <returns>List of results (task)</returns>
        public virtual async Task<List<TEntity>> AllMatchingAsync(Expression<Func<TEntity, bool>> filter, List<string> includes = null)
        {
           List<TEntity> items = null;

            try
            {
                items = await _unitOfWork.GetQueryable(includes, filter).ToListAsync();
            }
            catch (Exception ex)
            {
                //TODO:ExceptionFactory
            }

                return items;
        }

       
       

        /// <summary>
        /// Total count by filter
        /// </summary>
        /// <param name="filter">Expression filter</param>
        /// <param name="includes">Inners</param>
        /// <returns>Total count</returns>
        public virtual int AllMatchingCount(Expression<Func<TEntity, bool>> filter = null, List<string> includes = null)
        {
            var totalItems = 0;

            try
            {
                totalItems = _unitOfWork.GetQueryable(includes, filter).Count();
            }
            catch (Exception ex)
            {
                //TODO:ExceptionFactory
            }

            return totalItems;
        }

        /// <summary>
        /// Total count by filter async
        /// </summary>
        /// <param name="filter">Expression filter</param>
        /// <param name="includes">Inners</param>
        /// <returns>Total count (task)</returns>
        public virtual async Task<int> AllMatchingCountAsync(Expression<Func<TEntity, bool>> filter = null, List<string> includes = null)
        {
            var totalItems = 0;

            try
            {
                totalItems = await _unitOfWork.GetQueryable(includes, filter).CountAsync();
            }
            catch (Exception ex)
            {
                //TODO:ExceptionFactory
            }


            return totalItems;
        }

        /// <summary>
        /// Cleanup resources
        /// </summary>
        public void Dispose()
        {
            if (_unitOfWork != null)
                _unitOfWork.Dispose();
        }

        #endregion
    }
}
