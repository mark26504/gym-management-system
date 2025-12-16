using GymManagementSystemDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Repositories.Interfaces
{
    public interface IBookingRepository : IGenericRepository<MemberSession>
    {
        IEnumerable<MemberSession> GetSessionbyId(int sessionId);
    }
}
