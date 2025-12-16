using GymManagementSystemDAL.Entities.Inherited;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Entities
{
    public class Member : Person
    {
        // In Configuration
        // CreatedAt Column will be => JoinDate
        // Ignore => UpdatedAt
        public string Photo { get; set; } = null!;

        #region  [1 - 1] Member & HealtRecord
        //Nav
        public HealthRecord HealthRecord { get; set; }
        #endregion

        #region [M - M] Member & Plan
        //Nav Prop
        public ICollection<Membership> Members { get; set; }

        #endregion

        #region [M - M] Member & Session
        public ICollection<MemberSession> MemberSessions { get; set; }
        #endregion

    }
}
