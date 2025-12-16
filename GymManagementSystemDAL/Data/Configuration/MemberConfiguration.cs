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
    internal class MemberConfiguration : PersonConfiguration<Member>, IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.Property(P => P.CreatedAt)
                .HasColumnName("JoinDate")
                .HasDefaultValueSql("GETDATE()");

            builder.Ignore(P => P.UpdatedAt);
            //Must be after all configurations of MemberConfiguration

            base.Configure(builder);
        }
    }
}
