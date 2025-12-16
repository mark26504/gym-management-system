using System.ComponentModel.DataAnnotations;

namespace GymManagementSystemBLL.ViewModels.SessionViewModels
{
	public class CreateSessionViewModel
	{
		[Required(ErrorMessage = "Description is required")]
		[StringLength(500, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 500 characters")]
		public string Description { get; set; } = null!;

		[Required(ErrorMessage = "Capacity is required")]
		[Range(0, 25, ErrorMessage = "Capacity must be between 0 and 25")]
		public int Capacity { get; set; }

		[Required(ErrorMessage = "Start date is required")]
		[Display(Name = "Start Date & Time")]
		public DateTime StartDate { get; set; }

		[Required(ErrorMessage = "End date is required")]
		[Display(Name = "End Date & Time")]
		public DateTime EndDate { get; set; }

		[Required(ErrorMessage = "Trainer is required")]
		[Display(Name = "Trainer")]
		public int TrainerId { get; set; }

		[Required(ErrorMessage = "Category is required")]
		[Display(Name = "Category")]
		public int CategoryId { get; set; }
	}
}
