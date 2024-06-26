using Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DAL
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<StudentUser> Students { get; set; }
        public DbSet<TeacherUser> Teachers { get; set; }
        public DbSet<CordinatorUser> Cordinators { get; set; }
        public DbSet<AdminUser> Admins { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<LessonTime> LessonTimes { get; set; }
        public DbSet<GradeAndAttendace> GradeAndAttendaces { get; set; }
        public DbSet<Colloquium> Colloquia { get; set; }
        public DbSet<TermPaper> TermPapers { get; set; }


    }
}
