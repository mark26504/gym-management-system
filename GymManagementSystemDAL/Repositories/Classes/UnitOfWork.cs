using GymManagementSystemDAL.Data.Context;
using GymManagementSystemDAL.Entities.Inherited;
using GymManagementSystemDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Repositories.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();
        private readonly GymManagementSystemDbContext _dbContext;

        // 1. Inject DbContext
        // 2. Create Dictionaryt o Store Requests in it.
        // 3. Implement 2 Method
        public UnitOfWork(GymManagementSystemDbContext dbContext, ISessionRepository sessionRepository, IMembershipRepository membershipRepository, IBookingRepository bookingRepository)
        {
            _dbContext = dbContext;
            SessionRepository = sessionRepository;
            MembershipRepository = membershipRepository;
            BookingRepository = bookingRepository;
        }

        public ISessionRepository SessionRepository { get; }
        public IMembershipRepository MembershipRepository { get; }
        public IBookingRepository BookingRepository { get; }



        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new()
        {
            var EntityType = typeof(TEntity);

            if (_repositories.TryGetValue(EntityType, out var Repo))
                return (IGenericRepository<TEntity>) Repo;

            var NewRepo = new GenericRepository<TEntity> (_dbContext);
            _repositories[EntityType] = NewRepo;
            return NewRepo;
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }
    }
}
