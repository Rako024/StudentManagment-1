using Business.LayoutServices;
using Business.Services.Abstracts;
using Business.Services.Concretes;
using Core.Models;
using Core.RepositoryAbstracts;
using Data.DAL;
using Data.RepositoryConcretes;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Main;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllersWithViews();

        builder.Services.AddScoped<IGroupRepository, GroupRepository>();
        builder.Services.AddScoped<IGroupService,GroupService>();

        builder.Services.AddScoped<IStudentUserRepositroy,StudentUserRepository>();
        builder.Services.AddScoped<IStudentUserService,StudentUserService>();

        builder.Services.AddScoped<ITeacherUserRepository, TeacherUserRepository>();
        builder.Services.AddScoped<ITeacherUserService, TeacherUserService>();

        builder.Services.AddScoped<ILessonRepository, LessonRepository>();
        builder.Services.AddScoped<ILessonService, LessonService>();

        builder.Services.AddScoped<ILessonTimeRepository, LessonTimeRepository>();
        builder.Services.AddScoped<ILessonTimeService, LessonTimeService>();

        builder.Services.AddScoped<IGradeAndAttendaceRepository, GradeAndAttendaceRepository>();
        builder.Services.AddScoped<IGradeAndAttendaceService, GradeAndAttendaceService>();

        builder.Services.AddScoped<IColloquiumRepository, ColloquiumRepository>();
        builder.Services.AddScoped<IColloquiumService, ColloquiumService>();

        builder.Services.AddScoped<AdminLayoutService>();

        builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
        {
            opt.Password.RequireDigit = false;
            opt.Password.RequiredLength = 6; 
            opt.Password.RequireNonAlphanumeric = false;
            opt.Password.RequireUppercase = false;
            opt.Password.RequireLowercase = false;
            opt.User.RequireUniqueEmail = true;
        }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders(); ;

       
        builder.Services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
        });
        var app = builder.Build();

        app.UseStaticFiles();

        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
          );
        app.MapControllerRoute(
            name: "Default",
            pattern: "{controller=Home}/{action=Index}/{id?}"
            );

        app.Run();
    }
}
