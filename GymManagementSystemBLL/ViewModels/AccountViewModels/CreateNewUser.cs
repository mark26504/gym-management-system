using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemBLL.ViewModels.AccountViewModels
{
    public class CreateNewUser
    {
        [Display(Name = "firstName"), Required(ErrorMessage = "required")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "onlyENLetters")]
        [StringLength(50, ErrorMessage = "stringLengthMax")]
        public string FirstName { get; set; } = null!;
        [StringLength(50, ErrorMessage = "stringLengthMax")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "onlyENLetters")]
        [Display(Name = "lastName"), Required(ErrorMessage = "required")]
        public string LastName { get; set; } = null!;
        public string Username => $"{FirstName.Trim()}_{LastName.Trim()}{new Guid()}";


        [Display(Name = "email"), Required(ErrorMessage = "required")]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Display(Name = "password"), Required(ErrorMessage = "required")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "passwordMinLength")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z]).{6,}$",
            ErrorMessage = "PasswordUpperLower")]
        [Compare("ConfirmPassword", ErrorMessage = "PasswordNotMatch")]
        public string Password { get; set; } = null!;

        [Display(Name = "confirmPassword"), Required(ErrorMessage = "required")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;
        [Display(Name = "role"), Required(ErrorMessage = "required")]
        public Role Role { get; set; }
    }
    public enum Role
    {
        [Display(Name = "Admin")]
        Admin = 1,
        [Display(Name = "SuperAdmin")]

        SuperAdmin = 2
    }
}
