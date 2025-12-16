using GymManagementSystemDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Repositories.Interfaces
{
    public interface IMembershipRepository : IGenericRepository<Membership>
    {
        IEnumerable<Membership> GetAllMembershipsWithMembersAndPlans(Func<Membership, bool>? filter = null);
        Membership? GetFirstOrDefault(Func<Membership, bool>? filter = null);
    }
}
