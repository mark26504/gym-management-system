using GymManagementSystemDAL.Entities.Inherited;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Entities
{
    public class Plan : BaseEntity
    {
        /*
         * In Configuration: 
         *      - Ignore => UpdatedAt & CreatedAt
         */
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int DurationDays { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }

        #region [M - M] Member & Plan
        //Nav Prop
        public ICollection<Membership> Plans { get; set; }

        #endregion

    }
}
