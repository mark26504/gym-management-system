using GymManagementSystemDAL.Entities.Enums;
using GymManagementSystemDAL.Entities.Inherited;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Entities
{
    public class Trainer : Person
    {
        // In Configuration
        // CreatedAt Column will be => HireDate
        // Ignore => UpdatedAt
        public Specialties Specialties { get; set; }

        #region [1 - M] Trainer & Session
        public ICollection<Session> TrainerSessions { get; set; }
        #endregion
    }
}
