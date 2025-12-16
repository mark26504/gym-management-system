using AutoMapper;
using GymManagementSystemBLL.Services.Interfaces;
using GymManagementSystemBLL.ViewModels.BookingViewModels;
using GymManagementSystemBLL.ViewModels.MembershipsViewModels;
using GymManagementSystemBLL.ViewModels.SessionViewModels;
using GymManagementSystemDAL.Entities;
using GymManagementSystemDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemBLL.Services.Classes
{
    public class BookingServices : IBookingServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookingServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<MemberForSessionViewModel> GetAllMembersForSession(int id)
        {
            var bookingRepo = _unitOfWork.BookingRepository;
            var membersForSession = bookingRepo.GetSessionbyId(id);
            var memberForSessionViewModels = _mapper.Map<IEnumerable<MemberForSessionViewModel>>(membersForSession);
            return memberForSessionViewModels;
        }

        public IEnumerable<SessionViewModel> GetAllSessionsWithTrainerandCategory()
        {
            var sessionRepo = _unitOfWork.SessionRepository;
            var sessions = sessionRepo.GetAllSessionsWithTrainerAndCategory();
            var sessionViewModels = _mapper.Map<IEnumerable<SessionViewModel>>(sessions);

            foreach(var session in sessionViewModels)
                session.AvailableSlots = session.Capacity - sessionRepo.GetCountOfBookSlots(session.Id);

            return sessionViewModels;
        }

        public bool CreateBooking(CreateBookingViewModel model)
        {
            var session = _unitOfWork.SessionRepository.GetById(model.SessionId);

            if (session is null || session.StartDate <= DateTime.UtcNow)
                return false;

            var membershipRepo = _unitOfWork.MembershipRepository;
            var activeMembershipForMember = membershipRepo.GetFirstOrDefault(m => m.Status == "Active" && m.MemberId == model.MemberId);

            if (activeMembershipForMember is null)
                return false;

            var sessionRepo = _unitOfWork.SessionRepository;
            var bookedSlots = sessionRepo.GetCountOfBookSlots(model.SessionId);

            var avaliableSlots = session.Capacity - bookedSlots;
            if (avaliableSlots <= 0)
                return false;

            var booking = _mapper.Map<MemberSession>(model);
            booking.IsAttended = false;

            _unitOfWork.BookingRepository.Add(booking);
            return _unitOfWork.SaveChanges() > 0;
        }

        #region HelperMethods
        public IEnumerable<MemberForSelectListViewModel> GetMemberForDropdown(int id)
        {
            var bookingRepo = _unitOfWork.BookingRepository;
            var bookedMemberIds = bookingRepo.GetAll(s => s.Id == id)
                                             .Select(ms => ms.MemberId)
                                             .ToList();
            var membersAvailableToBook = _unitOfWork.GetRepository<Member>().GetAll(m => !bookedMemberIds.Contains(m.Id));

            var memberSelectList = _mapper.Map<IEnumerable<MemberForSelectListViewModel>>(membersAvailableToBook);

            return memberSelectList;
        }

        public bool MemberAttended(MemberAttendOrCancelViewModel model)
        {
            try
            {
                var memberSession = _unitOfWork.GetRepository<MemberSession>()
                                           .GetAll(X => X.MemberId == model.MemberId && X.SessionId == model.SessionId)
                                           .FirstOrDefault();
                if (memberSession is null) return false;

                memberSession.IsAttended = true;
                memberSession.UpdatedAt = DateTime.Now;
                _unitOfWork.GetRepository<MemberSession>().Update(memberSession);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool CancelBooking(MemberAttendOrCancelViewModel model)
        {
            try
            {
                var session = _unitOfWork.SessionRepository.GetById(model.SessionId);
                if (session is null || session.StartDate <= DateTime.Now) return false;

                // BUSINESS RULE #5: A booking can only be cancelled for future sessions. Once the session has started, cancellation is not allowed.
                var Booking = _unitOfWork.BookingRepository.GetAll(X => X.MemberId == model.MemberId && X.SessionId == model.SessionId)
                                                           .FirstOrDefault();
                if (Booking is null) return false;
                _unitOfWork.BookingRepository.Delete(Booking);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
