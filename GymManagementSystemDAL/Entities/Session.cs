using GymManagementSystemDAL.Entities.Inherited;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Entities
{
    public class Session : BaseEntity
    {
        /*
         * In Configuration: 
         *      - Ignore CreatedAt & UpdatedAt
         */
        public string Description { get; set; } = null!;
        public int Capacity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        #region [1 - M] Category & Session
        //FK
        public int CategoryId { get; set; }

        //Nav Prop
        public Category? Category { get; set; }
        #endregion

        #region [1 - M] Trainer & Session
        //FK
        public int TrainerId { get; set; }

        //Nav prop
        public Trainer? Trainer { get; set; }
        #endregion

        #region [M - M] Member & Session
        public ICollection<MemberSession> MemberSessions { get; set; }
        #endregion
    }
}
