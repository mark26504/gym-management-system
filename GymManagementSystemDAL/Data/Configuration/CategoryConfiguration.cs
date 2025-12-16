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
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(P => P.CategoryName)
                .HasColumnType("varchar")
                .HasMaxLength(20);

            builder.Ignore(P => P.CreatedAt);
            builder.Ignore(P => P.UpdatedAt);

        }
    }
}
