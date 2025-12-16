using GymManagementSystemDAL.Entities.Inherited;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Entities
{
    public class MemberSession : BaseEntity
    {
        public int MemberId { get; set; }
        public Member Member { get; set; }
        public int SessionId { get; set; }
        public Session Session { get; set; }

        public bool IsAttended { get; set; }
    }
}
