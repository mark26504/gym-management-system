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
    internal class TrainerConfiguration : PersonConfiguration<Trainer>, IEntityTypeConfiguration<Trainer>
    {
        public void Configure(EntityTypeBuilder<Trainer> builder)
        {
            builder.Property(P => P.CreatedAt)
                .HasColumnName("HireDate")
                .HasDefaultValueSql("GETDATE()");

            builder.Ignore(P => P.UpdatedAt);

            //Must be after all configurations of TrainerConfiguration
            base.Configure(builder);
        }
    }
}
