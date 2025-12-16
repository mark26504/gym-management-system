using GymManagementSystemDAL.Entities.Inherited;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Entities
{
    public class Category : BaseEntity
    {
        /*
         * In Configuration: 
         *      - Ignore => UpdatedAt & CreatedAt
         */
        public string CategoryName { get; set; } = null!;

        #region [1 - M] Category & Session
        //Nav Prop
        public ICollection<Session> SessionsCate { get; set; }

        #endregion
    }
}
