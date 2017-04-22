using CVBot.BussinessLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CVBot.DataAccess.Repository
{
    public class ModelUnitOfWork : CvBotDBEntities, IUnitOfWork
    {
        #region IQueryableUnitOfWork

        public DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class
        {
            return Set<TEntity>();
        }

        public IQueryable<TEntity> GetQueryable<TEntity>(List<string> includes = null, Expression<Func<TEntity, bool>> filter = null, int pageGo = 0, int pageSize = 0, List<string> orderBy = null, bool orderAscendent = false) where TEntity : class
        {
            IQueryable<TEntity> items = Set<TEntity>();

            if (includes != null && includes.Any())
                includes.Where(i => i != null).ToList().ForEach(i => { items = items.Include(i); });

            if (filter != null)
                items = items.Where(filter);

            if (pageSize > 0)
            {
                if (orderBy != null && orderBy.Any())
                {
                    orderBy.Where(i => i != null).ToList().ForEach(s => items = QueryableUtils.CallOrderBy(items, s, orderAscendent));
                    items = items.Skip(pageSize * (pageGo - 1));
                }
                items = items.Take(pageSize);
            }

            return items;
        }

        public void SetEntryState<TEntity>(TEntity item, EntityState state) where TEntity : class
        {
            Entry(item).State = state;
        }

        public void ApplyCurrentValues<TEntity>(TEntity original, TEntity current) where TEntity : class
        {
            Entry(original).CurrentValues.SetValues(current);
        }

        public int Commit()
        {
            return base.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await base.SaveChangesAsync();
        }

        public IQueryable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters)
        {
            return Database.SqlQuery<TEntity>(sqlQuery, parameters).AsQueryable();
        }

        public int ExecuteCommand(string sqlCommand, params object[] parameters)
        {
            return Database.ExecuteSqlCommand(sqlCommand, parameters);
        }

        public async Task<int> ExecuteCommandAsync(string sqlCommand, params object[] parameters)
        {
            return await Database.ExecuteSqlCommandAsync(sqlCommand, parameters);
        }

        public void SetConnectionDb(string connectionstring)
        {
            if (!string.IsNullOrEmpty(connectionstring))
                Database.Connection.ConnectionString = connectionstring;
        }

        public int CommitAndRefreshChanges()
        {
            var result = 0;
            bool saveFailed;

            do
            {
                try
                {
                    result = base.SaveChanges();
                    saveFailed = false;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;
                    ex.Entries.ToList().ForEach(entry => entry.OriginalValues.SetValues(entry.GetDatabaseValues()));
                }
            } while (saveFailed);

            return result;
        }

        public async Task<int> CommitAndRefreshChangesAsync()
        {
            var result = 0;
            bool saveFailed;

            do
            {
                try
                {
                    result = await base.SaveChangesAsync();
                    saveFailed = false;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;
                    ex.Entries.ToList().ForEach(entry => entry.OriginalValues.SetValues(entry.GetDatabaseValuesAsync()));
                }
            } while (saveFailed);

            return result;
        }

        public void RollBackChanges()
        {
            ChangeTracker.Entries().ToList().ForEach(entry => entry.State = EntityState.Unchanged);
        }

        public TEntity GetExistingEntityInContextOrNull<TEntity>(TEntity entity)
            where TEntity : class
        {
            var changedEntries = ChangeTracker.Entries<TEntity>();

            foreach (var entry in changedEntries)
            {
                var setBase = ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager
                    .GetObjectStateEntry(entry.Entity).EntitySet;

                var keyNames = setBase.ElementType.KeyMembers.Select(k => k.Name);

                var coincidence = true;
                foreach (var keyName in keyNames)
                {
                    var contextEntityValue = entry.Property(keyName).CurrentValue;
                    var entityValue = entity.GetType().GetProperty(keyName).GetValue(entity);

                    if (!contextEntityValue.Equals(entityValue))
                        coincidence = false;

                    if (!coincidence)
                        break;
                }
                if (coincidence)
                    return entry.Entity;
            }

            return null;
        }

        #endregion
    }
}