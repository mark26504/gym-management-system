using GymManagementSystemBLL.ViewModels.TrainerViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemBLL.Services.Interfaces
{
    public interface ITrainerServices
    {
        //Get All Trainers
        IEnumerable<TrainerViewModel> GetAllTrainers();

        // Create Trainer
        bool CreateTrainer(CreateTrainerViewModel createTrainer);

        // Get Trainer Details
        TrainerViewModel? GetTrainerDetails(int trainerId);

        //The Data That Return To User To Update
        UpdateTrainerViewModel? GetTrainerToUpdate(int trainerId);

        //Update Trainer Details
        bool UpdateTrainerDetails(int trainerId, UpdateTrainerViewModel updatedTrainer);

        //Delete Trainer
        bool RemoveTrainer(int trainerId);
    }
}
