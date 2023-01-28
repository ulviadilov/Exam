using BizLand.Areas.Manage.Services;
using BizLand.DAL;
using BizLand.Helpers;
using BizLand.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BizLand
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<BizLandContext>( opt =>
            {
                opt.UseSqlServer("Server=DESKTOP-8LGITHD;Database=BizLandDB;Trusted_Connection=True");
            });

            builder.Services.AddIdentity<AppUser, IdentityRole>( opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 8;
                opt.Password.RequireDigit = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireDigit = true;

                opt.User.RequireUniqueEmail = false;
            }).AddEntityFrameworkStores<BizLandContext>().AddDefaultTokenProviders();

            builder.Services.AddScoped<AdminLayoutService>();
            builder.Services.AddScoped<SettingService>();


            var app = builder.Build();

            

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
          );

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}