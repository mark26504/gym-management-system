namespace GymManagementSystemBLL.ViewModels.BookingViewModels
{
    public class MemberForSessionViewModel
    {
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public int SessionId { get; set; }
        public string BookingDate { get; set; }
        public bool IsAttended { get; set; }
    }
}
