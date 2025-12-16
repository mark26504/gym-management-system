using GymManagementSystemDAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Data.Context
{
    public class GymManagementSystemDbContext : IdentityDbContext<ApplicationUser>
    {
        //In AppSettings.json => Connection String
        ///protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        ///{
        ///    optionsBuilder.UseSqlServer("Server = .; Database = GymManagementSystem; Trusted_Connection = true; TrustServerCertificate = true");
        ///}

        public GymManagementSystemDbContext(DbContextOptions<GymManagementSystemDbContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<ApplicationUser>(AU =>
            { 
                AU.Property(a => a.FirstName)
                .HasColumnType("varchar")
                .HasMaxLength(50);

                AU.Property(a => a.LastName)
                .HasColumnType("varchar")
                .HasMaxLength(50);
            });
        }

        #region Tables

        public DbSet<Member> Members { get; set; }
        public DbSet<HealthRecord> HealthRecords { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<MemberSession> MemberSessions { get; set; }

        #endregion
    }
}
