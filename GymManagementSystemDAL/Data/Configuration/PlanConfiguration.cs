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
    internal class PlanConfiguration : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.Property(P => P.Name)
                .HasColumnType("varchar")
                .HasMaxLength(50);

            builder.Property(P => P.Description)
                .HasColumnType("varchar")
                .HasMaxLength(200);
            builder.Property(P => P.Price)
                .HasPrecision(10, 2);

            builder.Ignore(P => P.CreatedAt);
            builder.Ignore(P => P.UpdatedAt);


            builder.ToTable(Tb => 
            {
                Tb.HasCheckConstraint("CK_Plan_DurationDays", "DurationDays Between 1 and 365");
            });
        }
    }
}
