using GymManagementSystemDAL.Data.Context;
using GymManagementSystemDAL.Entities;
using GymManagementSystemDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Repositories.Classes
{
    public class PlanRepository : IPlanRepository
    {
        private readonly GymManagementSystemDbContext _dbContext;

        public PlanRepository(GymManagementSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Plan>? GetAll() => _dbContext.Plans.ToList();

        public Plan? GetById(int id) => _dbContext.Plans.Find(id);

        public int Update(Plan plan)
        {
            _dbContext.Plans.Update(plan);
            return _dbContext.SaveChanges();
        }
    }
}
