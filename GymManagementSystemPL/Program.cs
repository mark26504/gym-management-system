using GymManagementSystemBLL.AutoMapper;
using GymManagementSystemBLL.Services.AttachmentService;
using GymManagementSystemBLL.Services.Classes;
using GymManagementSystemBLL.Services.Interfaces;
using GymManagementSystemDAL.Data.Context;
using GymManagementSystemDAL.Data.DataSeed;
using GymManagementSystemDAL.Entities;
using GymManagementSystemDAL.Repositories.Classes;
using GymManagementSystemDAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystemPL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            #region Dependency Injection
            //DbContext Class => Public
            builder.Services.AddDbContext<GymManagementSystemDbContext>(options => 
            {
                //options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings")["DefaultConnection"]);
                //options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            #endregion

            builder.Services.AddScoped(typeof(IGenericRepository<>) , typeof(GenericRepository<>));
            builder.Services.AddScoped<IPlanRepository, PlanRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<ISessionRepository, SessionRepository>();
            builder.Services.AddScoped<IMembershipRepository, MembershipRepository>();
            builder.Services.AddScoped<IBookingRepository, BookingRepository>();

            builder.Services.AddAutoMapper(X => X.AddProfile(new MappingProfiles()));

            builder.Services.AddScoped<IAnalyticsServices, AnalyticsServices>();

            builder.Services.AddScoped<IMemberServices, MemberServices>();
            builder.Services.AddScoped<ITrainerServices, TrainerServices>();
            builder.Services.AddScoped<IPlanServices, PlanServices>();
            builder.Services.AddScoped<ISessionServices, SessionServices>();
            builder.Services.AddScoped<IMembershipServices, MembershipServices>();
            builder.Services.AddScoped<IAttachmentService, AttachmentService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IBookingServices, BookingServices>();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(Config => 
            {
                Config.Password.RequiredLength = 6;
                Config.Password.RequireLowercase = true;
                Config.Password.RequireUppercase = true;
                Config.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<GymManagementSystemDbContext>();

            builder.Services.ConfigureApplicationCookie(options => 
            {
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
            });
            var app = builder.Build();

            //Must Seed Data After Build()

            #region Seed Data
            var Scope = app.Services.CreateScope();
            var dbContext = Scope.ServiceProvider.GetRequiredService<GymManagementSystemDbContext>();

            //Check if ther is any pending migration
            var PendingMigrations = dbContext.Database.GetPendingMigrations();
            if (PendingMigrations?.Any() ?? false)
                dbContext.Database.Migrate();

            GymContextSeeding.SeedDate(dbContext);

            var roleManager = Scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = Scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            IdentityDbContextSeeding.SeedData(roleManager, userManager);
            #endregion

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=login}/{id?}")
                .WithStaticAssets();
              
            app.Run();
        }
    }
}
