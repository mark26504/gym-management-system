using GymManagementSystemDAL.Entities.Inherited;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        // Call IUnitOfWork => For Ask Specific Repository
        // Has 2 Methods:
        //      1. Method to Get Repository
        //      2. Method to Save Changes

        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity , new();

        int SaveChanges();

        public ISessionRepository SessionRepository { get;}
        public IMembershipRepository MembershipRepository { get; }
        public IBookingRepository BookingRepository { get; }
    }
}
