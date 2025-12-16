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
    public class SessionRepository : GenericRepository<Session>, ISessionRepository
    {
        private readonly GymManagementSystemDbContext _dbContext;
        public SessionRepository(GymManagementSystemDbContext dbContext): base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Session> GetAllSessionsWithTrainerAndCategory()
        {
            return _dbContext.Sessions
                .Include(S => S.Trainer)
                .Include(S => S.Category)
                .ToList();
        }

        public int GetCountOfBookSlots(int SessionId)
        {
            return _dbContext.MemberSessions.Count(X => X.SessionId == SessionId);
        }

        public Session? GetSessionWithTrainerAndCategoryById(int id)
        {
            return _dbContext.Sessions
                .Include(S => S.Trainer)
                .Include(S => S.Category)
                .FirstOrDefault(S => S.Id == id);
        }
    }
}
