using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Entities
{
    [Owned]
    public class Address
    {
        public int BuildingNumber { get; set; }
        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;
    }
}
