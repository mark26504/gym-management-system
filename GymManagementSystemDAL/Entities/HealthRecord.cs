using GymManagementSystemDAL.Entities.Inherited;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Entities
{
    public class HealthRecord : BaseEntity
    {
        /*
         * In Configuration: 
         *      - UpdatedAt Column will be => LastUpdate
         *      - Ignore => Id & CreatedAt
         */
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public string BloodType { get; set; } = null!;
        public string? Note { get; set; }

        #region  [1 - 1] Member & HealtRecord
        //Nav
        public Member MemberHealth { get; set; }
        #endregion

    }
}
