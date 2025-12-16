using GymManagementSystemDAL.Data.Context;
using GymManagementSystemDAL.Entities.Inherited;
using GymManagementSystemDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Repositories.Classes
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity, new()
    {
        private readonly GymManagementSystemDbContext dbContext;

        public GenericRepository(GymManagementSystemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(TEntity entity)
        {
            dbContext.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {
            dbContext.Set<TEntity>().Remove(entity);
        }

        public IEnumerable<TEntity> GetAll(Func<TEntity, bool>? condition = null)
        {
            if (condition is null)
                return dbContext.Set<TEntity>().AsNoTracking().ToList();
            else
                return dbContext.Set<TEntity>().AsNoTracking().Where(condition).ToList();
        }

        public TEntity? GetById(int id) => dbContext.Set<TEntity>().Find(id);

        public void Update(TEntity entity)
        {
            dbContext.Set<TEntity>().Update(entity);
        }
    }
}
