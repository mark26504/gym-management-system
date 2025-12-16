using GymManagementSystemBLL.Services.Interfaces;
using GymManagementSystemBLL.ViewModels.TrainerViewModels;
using GymManagementSystemDAL.Entities;
using GymManagementSystemDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemBLL.Services.Classes
{
    public class TrainerServices : ITrainerServices
    {
        #region Connection

        #region Feilds

        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region CTOR
        public TrainerServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        #endregion

        //Get All Trainers
        public IEnumerable<TrainerViewModel> GetAllTrainers()
        {
            var Trainers = _unitOfWork.GetRepository<Trainer>().GetAll();

            var TrainerViewModels = Trainers.Select(trainer => new TrainerViewModel()
            {
                Id = trainer.Id,
                Name = trainer.Name,
                Email = trainer.Email,
                Phone = trainer.Phone,
                Specialization = trainer.Specialties,
            });

            return TrainerViewModels;
        }

        // Create Trainer
        public bool CreateTrainer(CreateTrainerViewModel createTrainer)
        {
            try
            {
                if (IsEmailExist(createTrainer.Email) || IsPhoneExist(createTrainer.Phone)) return false;

                var trainer = new Trainer()
                {
                    Name = createTrainer.Name,
                    Email = createTrainer.Email,
                    Phone = createTrainer.Phone,
                    DateOfBirth = createTrainer.DateOfBirth,
                    Gender = createTrainer.Gender,

                    Address = new Address()
                    {
                        BuildingNumber = createTrainer.BuildingNumber,
                        Street = createTrainer.Street,
                        City = createTrainer.City,
                    },

                    Specialties = createTrainer.Specialization,
                };

                _unitOfWork.GetRepository<Trainer>().Add(trainer);
                return _unitOfWork.SaveChanges() > 0;

            }
            catch (Exception)
            {
                return false;
            }
        }

        //Get Trainer Details
        public TrainerViewModel? GetTrainerDetails(int trainerId)
        {
            var trainer = _unitOfWork.GetRepository<Trainer>().GetById(trainerId);
            if (trainer is null) return null;

            var TrainerDetails = new TrainerViewModel()
            {
                Id = trainer.Id,
                Name = trainer.Name,
                Email = trainer.Email,
                Phone = trainer.Phone,
                Specialization = trainer.Specialties,
                DateOfBirth = trainer.DateOfBirth.ToString("dd/MM/yyyy"),
                Address = $"{trainer.Address.BuildingNumber}, {trainer.Address.Street}, {trainer.Address.City}",
            };

            return TrainerDetails;
        }

        //The Data That Return To User To Update
        public UpdateTrainerViewModel? GetTrainerToUpdate(int trainerId)
        {
            var trainer = _unitOfWork.GetRepository<Trainer>().GetById(trainerId);
            if (trainer is null) return null;

            return new UpdateTrainerViewModel()
            {
                Name = trainer.Name,
                Email = trainer.Email,
                Phone = trainer.Phone,
                BuildingNumber = trainer.Address.BuildingNumber,
                Street = trainer.Address.Street,
                City = trainer.Address.City,
                Specialization = trainer.Specialties,
            };
        }

        //Update Trainer Details
        public bool UpdateTrainerDetails(int trainerId, UpdateTrainerViewModel updatedTrainer)
        {
            try
            {
                //if (IsEmailExist(updatedTrainer.Email) || IsPhoneExist(updatedTrainer.Phone)) return false;
                var emailExists = _unitOfWork.GetRepository<Trainer>()
                    .GetAll(X => X.Email == updatedTrainer.Email && X.Id != trainerId);

                var phoneExists = _unitOfWork.GetRepository<Trainer>()
                    .GetAll(X => X.Phone == updatedTrainer.Phone && X.Id != trainerId);

                if (emailExists.Any() || phoneExists.Any()) return false;

                var TrainerRepo = _unitOfWork.GetRepository<Trainer>();
                var TrainerToUpdate = TrainerRepo.GetById(trainerId);
                if (TrainerToUpdate is null) return false;

                TrainerToUpdate.Email = updatedTrainer.Email;
                TrainerToUpdate.Phone = updatedTrainer.Phone;
                TrainerToUpdate.Address.BuildingNumber = updatedTrainer.BuildingNumber;
                TrainerToUpdate.Address.Street = updatedTrainer.Street;
                TrainerToUpdate.Address.City = updatedTrainer.City;
                TrainerToUpdate.Specialties = updatedTrainer.Specialization;
                TrainerToUpdate.UpdatedAt = DateTime.Now;

                TrainerRepo.Update(TrainerToUpdate);
                return _unitOfWork.SaveChanges() > 0;

            }
            catch (Exception)
            {
                return false;
            }
        }

        //Delete Trainer
        public bool RemoveTrainer(int trainerId)
        {
            var TrainerRepo = _unitOfWork.GetRepository<Trainer>();
            var SessionRepo = _unitOfWork.GetRepository<Session>();
            var trainer = TrainerRepo.GetById(trainerId);
            if (trainer is null) return false;

            var HasFutureSessions = SessionRepo
                .GetAll(X => X.TrainerId == trainerId && X.StartDate > DateTime.Now)
                .Any();
            if (HasFutureSessions) return false;

            TrainerRepo.Delete(trainer);
            return _unitOfWork.SaveChanges() > 0;
        }

        #region Helper Method

        public bool IsEmailExist(string email)
        {
            return _unitOfWork.GetRepository<Trainer>().GetAll(X => X.Email == email).Any();
        }

        public bool IsPhoneExist(string phone)
        {
            return _unitOfWork.GetRepository<Trainer>().GetAll(X => X.Phone == phone).Any();
        }

        #endregion

    }
}
