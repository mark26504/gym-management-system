using GymManagementSystemDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Data.Configuration
{
    internal class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.ToTable(Tb => 
            {
                Tb.HasCheckConstraint("CK_Session_Capacity", "Capacity Between 1 and 25");
                Tb.HasCheckConstraint("CK_Session_Date", "EndDate > StartDate");
            });

            builder.Ignore(P => P.CreatedAt);
            builder.Ignore(P => P.UpdatedAt);

            #region [1 - M] Category & Session

            builder.HasOne(S => S.Category)
                .WithMany(C => C.SessionsCate)
                .HasForeignKey(S => S.CategoryId);

            #endregion

            #region [1 - M] Trainer & Session

            builder.HasOne(S => S.Trainer)
                .WithMany(T => T.TrainerSessions)
                .HasForeignKey(S => S.TrainerId);

            #endregion
        }
    }
}
