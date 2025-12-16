using GymManagementSystemDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Repositories.Interfaces
{
    public interface ISessionRepository : IGenericRepository<Session>
    {
        IEnumerable<Session> GetAllSessionsWithTrainerAndCategory();
        int GetCountOfBookSlots(int SessionId);
        Session? GetSessionWithTrainerAndCategoryById(int id);
    }
}
