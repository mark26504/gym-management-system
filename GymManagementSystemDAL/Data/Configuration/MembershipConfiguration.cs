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
    internal class MembershipConfiguration : IEntityTypeConfiguration<Membership>
    {
        public void Configure(EntityTypeBuilder<Membership> builder)
        {
            builder.Property(Ms => Ms.CreatedAt)
                .HasColumnName("StartDate")
                .HasDefaultValueSql("GETDATE()");

            builder.HasKey(Ms => new
            {
                Ms.MemberId , Ms.PlanId
            });

            builder.Ignore(Ms => Ms.Id);
            builder.Ignore(Ms => Ms.UpdatedAt);
        }
    }
}
