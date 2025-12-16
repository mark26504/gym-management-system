using GymManagementSystemDAL.Entities.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemBLL.ViewModels.MemberViewModels
{
    public class CreateMemberViewModel
    {

        [Required(ErrorMessage = "Photo is required.")]
        [Display(Name = "Profile Photo")]
        public IFormFile Photo { get; set; } = null!;

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, MinimumLength = 2 , ErrorMessage ="Name Must Be Between 2 and 50 Chars.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name Contain Only letters!")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage ="Invaild Email Format!")]
        [StringLength(100, MinimumLength = 5 , ErrorMessage ="Email Must Be Between 5 and 100 Chars.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Phone is required.")]
        [Phone(ErrorMessage = "Invaild Phone Format!")]
        [RegularExpression(@"^(010|011|012|015)\d{8}")]
        public string Phone { get; set; } = null!;

        [Required(ErrorMessage ="Required!")]
        [DataType(DataType.Date)]
        public DateOnly DateOfBirth { get; set; }

        [Required(ErrorMessage = "Required!")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Required!")]
        [Range(1,9000,ErrorMessage ="Building Number Begin With 1 ")]
        public int BuildingNumber { get; set; }

        [Required(ErrorMessage = "Required!")]
        [StringLength(30, MinimumLength = 2, ErrorMessage ="Must Be Between 2 and 30")]
        public string Street { get; set; } = null!;

        [Required(ErrorMessage = "Required!")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "City Contain Only letters!")]
        public string City { get; set; } = null!;

        //HealthViewModel
        public HealthViewModel HealthViewModel { get; set; } = null!;
    }
}
