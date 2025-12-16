using GymManagementSystemDAL.Data.Context;
using GymManagementSystemDAL.Entities;
using GymManagementSystemDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Repositories.Classes
{
    public class BookingRepository : GenericRepository<MemberSession>, IBookingRepository
    {
        private readonly GymManagementSystemDbContext _dbContext;

        public BookingRepository(GymManagementSystemDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<MemberSession> GetSessionbyId(int sessionId)
        {
            return _dbContext.MemberSessions.Where(ms => ms.SessionId == sessionId)
                                            .Include(ms => ms.Member)
                                            .ToList();
        }
    }
}
