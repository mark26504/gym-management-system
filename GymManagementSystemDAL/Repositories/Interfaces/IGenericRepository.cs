using GymManagementSystemDAL.Entities;
using GymManagementSystemDAL.Entities.Inherited;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity, new()
    {
        //Get All Members
        IEnumerable<TEntity> GetAll(Func<TEntity, bool>? condition = null);

        //Get Member By Id
        TEntity? GetById(int id);

        //Add Member
        void Add(TEntity entity);

        //Update Member
        void Update(TEntity entity);
        //Delete Member
        void Delete(TEntity entity);

    }
}
