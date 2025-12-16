using GymManagementSystemDAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemBLL.ViewModels.TrainerViewModels
{
    public class TrainerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public Specialties Specialization { get; set; }

        //Get Trainer Details
        public string? Address { get; set; }
        public string? DateOfBirth { get; set; }
    }
}
