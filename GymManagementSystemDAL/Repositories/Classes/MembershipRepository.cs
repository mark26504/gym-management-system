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
    public class MembershipRepository : GenericRepository<Membership>, IMembershipRepository
    {
        private readonly GymManagementSystemDbContext _dbContext;

        public MembershipRepository(GymManagementSystemDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Membership> GetAllMembershipsWithMembersAndPlans(Func<Membership, bool>? filter = null)
        {
            var Memberships = _dbContext.Memberships.Include(m => m.Member)
                                                    .Include(m => m.Plan)
                                                    .Where(filter ?? (_ => true));

            return Memberships;
        }

        public Membership? GetFirstOrDefault(Func<Membership, bool>? filter = null)
        {
            var membership = _dbContext.Memberships.FirstOrDefault(filter ?? (_ => true));
            return membership;
        }
    }
}
