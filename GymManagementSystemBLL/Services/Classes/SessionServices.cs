using AutoMapper;
using GymManagementSystemBLL.Services.Interfaces;
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
    public class SessionServices : ISessionServices
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SessionServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<SessionViewModel> GetAllSessions()
        {
            //var Sessions = _unitOfWork.GetRepository<Session>().GetAll() ?? [];

            var Sessions = _unitOfWork.SessionRepository.GetAllSessionsWithTrainerAndCategory() ?? [];
            return Sessions.Select(s => new SessionViewModel
            {
                Id = s.Id,
                Description = s.Description,
                StartDate = s.StartDate,
                EndDate = s.EndDate,
                Capacity = s.Capacity,
                CategoryName = s.Category.CategoryName, // Related Data
                TrainerName = s.Trainer.Name,           // Related Data
                AvailableSlots = s.Capacity - _unitOfWork.SessionRepository.GetCountOfBookSlots(s.Id)
            });
        }

        public SessionViewModel? GetSessionById(int id)
        {
            var Session = _unitOfWork.SessionRepository.GetSessionWithTrainerAndCategoryById(id);
            if (Session == null) return null;

            //Allow AutoMapper to handle the mappinp
            var MappedSession = _mapper.Map<Session, SessionViewModel>(Session);
            MappedSession.AvailableSlots = MappedSession.Capacity - _unitOfWork.SessionRepository.GetCountOfBookSlots(MappedSession.Id);
            return MappedSession;
        }

        public bool CreateSession(CreateSessionViewModel createdSession)
        {
            try
            {
                // Check if Trainer and Category Exists?
                // Check if StartDate Before EndDate? 
                if (!IsTrainerExists(createdSession.TrainerId) ||
                    !IsCategoryExists(createdSession.CategoryId) ||
                    !IsDateTimeValid(createdSession.StartDate, createdSession.EndDate))
                    return false;
                if (createdSession.Capacity > 25 || createdSession.Capacity < 1) return false;

                var SesstionEntity = _mapper.Map<Session>(createdSession);
                _unitOfWork.GetRepository<Session>().Add(SesstionEntity);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public UpdateSessionViewModel? GetSessionToUpdate(int sessionId)
        {
            var session = _unitOfWork.GetRepository<Session>().GetById(sessionId);

            if (!IsSessionAvailableToUpdate(session!)) return null;

            return _mapper.Map<UpdateSessionViewModel>(session);
        }

        public bool UpdateSession(UpdateSessionViewModel updatedSession, int sessionId)
        {
            try
            {
                var Session = _unitOfWork.SessionRepository.GetById(sessionId);
                if (!IsSessionAvailableToUpdate(Session!)) return false;
                if (!IsTrainerExists(updatedSession.TrainerId)) return false;
                if (!IsDateTimeValid(updatedSession.StartDate, updatedSession.EndDate)) return false;

                _mapper.Map(updatedSession, Session);
                Session!.UpdatedAt = DateTime.Now;
                _unitOfWork.SessionRepository.Update(Session!);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Update Session Faild: {ex}");
                return false;
            }
        }

        public bool RemoveSession(int sessionId)
        {
            try
            {
                var Session = _unitOfWork.SessionRepository.GetById(sessionId);
                if (!IsSessionAvailableToDelete(Session!)) return false;

                _unitOfWork.SessionRepository.Delete(Session!);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public IEnumerable<TrainerSelectViewModel> GetTrainerForSession()
        {
            var Trainers = _unitOfWork.GetRepository<Trainer>().GetAll() ?? [];

            return _mapper.Map<IEnumerable<TrainerSelectViewModel>>(Trainers);
        }

        public IEnumerable<CategorySelectViewModel> GetCategoryForSession()
        {
            var Categories = _unitOfWork.GetRepository<Category>().GetAll() ?? [];
            return _mapper.Map<IEnumerable<CategorySelectViewModel>>(Categories);
        }

        public SessionViewModel? GetSessionToDelete(int id)
        {
            var session = _unitOfWork.SessionRepository.GetSessionWithTrainerAndCategoryById(id);
            if (session is null) return null;

            if (!IsSessionAvailableToDelete(session)) return null;

            return _mapper.Map<SessionViewModel>(session);
        }


        #region Helper Methods

        private bool IsTrainerExists(int trainerId) 
            => _unitOfWork.GetRepository<Trainer>().GetById(trainerId) is not null;
        

        private bool IsCategoryExists(int categoryId)
            => _unitOfWork.GetRepository<Category>().GetById(categoryId) is not null;

        private bool IsDateTimeValid(DateTime start, DateTime end)
            => start < end;

        private bool IsSessionAvailableToUpdate(Session session)
        {
            if (session is null) return false;

            // If Session is Completed => Not Available to Update
            if (session.EndDate < DateTime.Now) return false;

            // If Session is Ongoing => Not Available to Update
            if (session.StartDate <= DateTime.Now) return false;

            // If Session Has Active Bookings => Not Available to Update
            var ActiveBookings = _unitOfWork.SessionRepository.GetCountOfBookSlots(session.Id);
            if (ActiveBookings > 0) return false;

            return true;
        }
        private bool IsSessionAvailableToDelete(Session session)
        {
            if (session is null) return false;

            //// If Session is Completed => Not Available to Update
            //if (session.EndDate < DateTime.Now) return false;

            // If Session is Ongoing => Not Available to Update
            if (session.StartDate <= DateTime.Now && session.EndDate > DateTime.Now) return false;

            // If Session Has Active Bookings => Not Available to Update
            var ActiveBookings = _unitOfWork.SessionRepository.GetCountOfBookSlots(session.Id);
            if (ActiveBookings > 0) return false;

            return true;
        }


        #endregion
    }
}
