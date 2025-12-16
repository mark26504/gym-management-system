using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemBLL.ViewModels.PlanViewModels
{
    public class UpdatePlanViewModel
    {
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Required.")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "You Must Enter Description in Range 5 : 200 CHars!")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Required.")]
        [Range(1, 365, ErrorMessage = "Days Mast Be Between 1 and 365")]
        public int DurationDays { get; set; }

        [Required(ErrorMessage = "Required.")]
        [Range(1, 10000, ErrorMessage = "Price Must Be Greater Than 1 Pound")]
        public decimal Price { get; set; }
    }
}