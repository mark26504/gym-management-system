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
    internal class MemberSessionConfiguration : IEntityTypeConfiguration<MemberSession>
    {
        public void Configure(EntityTypeBuilder<MemberSession> builder)
        {
            builder.Ignore(MC => MC.Id);
            builder.Ignore(MC => MC.UpdatedAt);

            builder.HasKey(MC => new
            {
                MC.MemberId, MC.SessionId
            });

            builder.Property(MC => MC.CreatedAt)
                .HasColumnName("BookingDate")
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
