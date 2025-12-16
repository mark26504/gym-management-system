using AutoMapper;
using GymManagementSystemBLL.ViewModels.BookingViewModels;
using GymManagementSystemBLL.ViewModels.MembershipsViewModels;
using GymManagementSystemBLL.ViewModels.SessionViewModels;
using GymManagementSystemDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemBLL.AutoMapper
{
    public class MappingProfiles : Profile
    {
        // Profile Must be in CTOR
        public MappingProfiles()
        {
            //CreateMap<Session, SessionViewModel>();
            CreateMap<Session, SessionViewModel>()
                .ForMember(d => d.CategoryName, options => options.MapFrom(src => src.Category.CategoryName))
                .ForMember(d => d.TrainerName, options => options.MapFrom(src => src.Trainer.Name))
                .ForMember(d => d.AvailableSlots, options => options.Ignore());

            //CreateMap<CreateSessionViewModel, Session>();
            CreateMap<CreateSessionViewModel, Session>();
            CreateMap<UpdateSessionViewModel, Session>().ReverseMap();

            CreateMap<Trainer, TrainerSelectViewModel>();
            CreateMap<Category, CategorySelectViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CategoryName));

            CreateMap<Membership, MembershipViewModel>()
                .ForMember(dest => dest.MemberName, opt => opt.MapFrom(src => src.Member.Name))
                .ForMember(dest => dest.PlanName, opt => opt.MapFrom(src => src.Plan.Name))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.CreatedAt));

            CreateMap<CreateMembershipViewModel, Membership>();

            CreateMap<Plan, PlanForSelectListViewModel>();
            CreateMap<Member, MemberForSelectListViewModel>();

            CreateMap<MemberSession, MemberForSessionViewModel>()
                .ForMember(dest => dest.MemberName, opt => opt.MapFrom(src => src.Member.Name))
                .ForMember(dest => dest.BookingDate, opt => opt.MapFrom(src => src.CreatedAt));

            CreateMap<CreateBookingViewModel, MemberSession>();
        }
    }
}
