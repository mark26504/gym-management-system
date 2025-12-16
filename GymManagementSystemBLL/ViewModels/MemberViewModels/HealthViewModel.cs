using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemBLL.ViewModels.MemberViewModels
{
    public class HealthViewModel
    {
        [Required(ErrorMessage = "Required.")]
        [Range(30, 300, ErrorMessage = "Height must be between 30 cm and 300 cm.")]
        public decimal Height { get; set; }

        [Required(ErrorMessage = "Required.")]
        [Range(1, 500, ErrorMessage = "Weight must be between 1 kg and 500 kg.")]
        public decimal Weight { get; set; }

        [Required(ErrorMessage = "Required.")]
        [StringLength(3, ErrorMessage = "BloodType must be less than 3")]
        public string BloodType { get; set; }

        public string? Note { get; set; }
    }
}
