using GymManagementSystemBLL.Services.AttachmentService;
using GymManagementSystemBLL.Services.Interfaces;
using GymManagementSystemBLL.ViewModels.MemberViewModels;
using GymManagementSystemDAL.Entities;
using GymManagementSystemDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemBLL.Services.Classes
{
    public class MemberServices : IMemberServices
    {
        #region Connection

        #region Feilds

        private readonly IUnitOfWork _unitOfWork;
        private readonly IAttachmentService _attachmentService;

        #endregion

        #region CTOR
        public MemberServices(IUnitOfWork unitOfWork, IAttachmentService attachmentService)
        {
            _unitOfWork = unitOfWork;
            _attachmentService = attachmentService;
        }

        #endregion

        #endregion

        //Get All Members
        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            var Members = _unitOfWork.GetRepository<Member>().GetAll() ?? [];

            #region Way 01
            //var MemberViewModels = new List<MemberViewModel>();
            //foreach (var item in Members)
            //{
            //    var memberViewModel = new MemberViewModel()
            //    {
            //        Id = item.Id,
            //        Photo = item.Photo,
            //        Name = item.Name,
            //        Email = item.Email,
            //        Phone = item.Phone,
            //        Gender = item.Gender.ToString(),
            //    };
            //    MemberViewModels.Add(memberViewModel);
            //}
            //return MemberViewModels;
            #endregion

            #region Way 02
            var MemberViewModels = Members.Select(X => new MemberViewModel
            {
                Id = X.Id,
                Photo = X.Photo,
                Name = X.Name,
                Email = X.Email,
                Phone = X.Phone,
                Gender = X.Gender.ToString(),
            });
            #endregion
            return MemberViewModels;
        }

        public bool CreateMember(CreateMemberViewModel createMember)
        {
            try
            {
                //Check Phone and Email Unique
                //var isPhoneExist = _memberRepository.GetAll().Any(X => X.Phone == member.Phone);
                if (IsPhoneExist(createMember.Phone) || IsEmailExist(createMember.Email)) return false;

                var photoName = _attachmentService.Upload("members", createMember.Photo);
                if (photoName is null) return false;

                var member = new Member()
                {
                    Name = createMember.Name,
                    Email = createMember.Email,
                    Phone = createMember.Phone,
                    Gender = createMember.Gender,
                    DateOfBirth = createMember.DateOfBirth,
                    Address = new Address()
                    {
                        BuildingNumber = createMember.BuildingNumber,
                        Street = createMember.Street,
                        City = createMember.City,
                    },
                    HealthRecord = new HealthRecord()
                    {
                        Height = createMember.HealthViewModel.Height,
                        Weight = createMember.HealthViewModel.Weight,
                        BloodType = createMember.HealthViewModel.BloodType,
                        Note = createMember.HealthViewModel.Note,
                    }
                };
                member.Photo = photoName;

                _unitOfWork.GetRepository<Member>().Add(member);

                var isCreated = _unitOfWork.SaveChanges() > 0;
                if (!isCreated)
                {
                    _attachmentService.Delete("members", photoName);
                    return false;
                }

                return isCreated;

            }
            catch (Exception)
            {
                return false;
            }

        }

        public MemberViewModel? GetMemberDetails(int MemberId)
        {
            var Member = _unitOfWork.GetRepository<Member>().GetById(MemberId);
            if (Member is null) return null;

            var MemberViewModel = new MemberViewModel()
            {
                Name = Member.Name,
                Email = Member.Email,
                Phone = Member.Phone,
                Gender = Member.Gender.ToString(),
                DateOfBirth = Member.DateOfBirth.ToShortDateString(),
                Address = $"{Member.Address.BuildingNumber} - {Member.Address.Street} - {Member.Address.City}",
                Photo = Member.Photo,
            };

            //Active Membership
            var Membership = _unitOfWork.GetRepository<Membership>().GetAll(X => X.MemberId == MemberId && X.Status == "Active")
                                                  .FirstOrDefault();

            if (Membership is not null)
            {
                MemberViewModel.MembershipStratDate = Membership.CreatedAt.ToShortDateString();
                MemberViewModel.MembershipEndDate = Membership.EndDate.ToShortDateString();

                var Plan = _unitOfWork.GetRepository<Plan>().GetById(Membership.PlanId);
                MemberViewModel.PlanName = Plan?.Name;
            }

            return MemberViewModel;
        }

        //The Form View to User
        public UpdateMemberViewModel? UpdateMemberById(int MemberId)
        {
            var Member = _unitOfWork.GetRepository<Member>().GetById(MemberId);
            if (Member is null) return null;

            return new UpdateMemberViewModel()
            {
                Name = Member.Name,
                Photo = Member.Photo,
                Email = Member.Email,
                Phone = Member.Phone,
                BuildingNumber = Member.Address.BuildingNumber,
                Street = Member.Address.Street,
                City = Member.Address.City,
            };
        }

        //The Actual Update Method
        public bool UpdateMemberDetails(int MemberId, UpdateMemberViewModel updatedMember)
        {
            try
            {
                var MemberRepo = _unitOfWork.GetRepository<Member>();
                //if (IsPhoneExist(updatedMember.Phone) || IsEmailExist(updatedMember.Email)) return false;
                var emailExist = _unitOfWork.GetRepository<Member>()
                    .GetAll(X => X.Email == updatedMember.Email && X.Id != MemberId);
                var phoneExist = _unitOfWork.GetRepository<Member>()
                    .GetAll(X => X.Phone == updatedMember.Phone && X.Id != MemberId);

                if (emailExist.Any() || phoneExist.Any()) return false;

                var Member = MemberRepo.GetById(MemberId);
                if (Member is null) return false;

                Member.Name = updatedMember.Name;
                Member.Email = updatedMember.Email;
                Member.Phone = updatedMember.Phone;
                Member.Address.BuildingNumber = updatedMember.BuildingNumber;
                Member.Address.Street = updatedMember.Street;
                Member.Address.City = updatedMember.City;
                Member.UpdatedAt = DateTime.Now;

                MemberRepo.Update(Member);
                return _unitOfWork.SaveChanges() > 0;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteMember(int MemberId)
        {
            var MemberRepo = _unitOfWork.GetRepository<Member>();
            var MembershipRepo = _unitOfWork.GetRepository<Membership>();
            var MemberSessionRepo = _unitOfWork.GetRepository<MemberSession>();

            var Member = MemberRepo.GetById(MemberId);
            if (Member is null) return false;

            //Check if the member has any active memberships
            //var HasActiveSession =
            //    MemberSessionRepo.GetAll(X => X.MemberId == MemberId && X.Session.StartDate > DateTime.Now).Any();

            //Get All Session Ids
            var SessionIDs = _unitOfWork.GetRepository<MemberSession>()
                .GetAll(X => X.MemberId == MemberId).Select(X => X.SessionId);

            var HasActiveSession = _unitOfWork.GetRepository<Session>()
                .GetAll(X => SessionIDs.Contains(X.Id) && X.StartDate > DateTime.Now).Any();

            if (HasActiveSession) return false;

            var Membership = MembershipRepo.GetAll(X => X.MemberId == MemberId);
            try
            {
                if (Membership.Any())
                {
                    foreach (var member in Membership)
                    {
                        MembershipRepo.Delete(member);
                    }
                }
                MemberRepo.Delete(Member);
                var isDeleted =  _unitOfWork.SaveChanges() > 0;
                if (isDeleted)
                    _attachmentService.Delete("members", Member.Photo);
                
                return isDeleted;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public HealthViewModel? GetMemberHealthDetails(int MemberId)
        {
            var MemberHealthRecord = _unitOfWork.GetRepository<HealthRecord>().GetById(MemberId);

            if (MemberHealthRecord is null) return null;

            return new HealthViewModel()
            {
                Height = MemberHealthRecord.Height,
                Weight = MemberHealthRecord.Weight,
                BloodType = MemberHealthRecord.BloodType,
                Note = MemberHealthRecord.Note,
            };
        }

        #region Helper Methods

        private bool IsEmailExist(string email)
        {
            return _unitOfWork.GetRepository<Member>().GetAll(X => X.Email == email).Any();
        }
        private bool IsPhoneExist(string phone)
        {
            return _unitOfWork.GetRepository<Member>().GetAll(X => X.Phone == phone).Any();
        }

        #endregion
    }
}
