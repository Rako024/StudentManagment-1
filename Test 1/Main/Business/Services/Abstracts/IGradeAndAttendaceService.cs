using Business.DTOs.Teacher.TeacherLessonsDto;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstracts
{
    public interface IGradeAndAttendaceService
    {
        Task AddGradeAndAttendaceAsync(GradeAndAttendaceDto dto);
        Task UpdateGradeAndAttendaceAsync(GradeAndAttendaceDto dto);
        Task<List<GradeAndAttendace>> GetAllGradeAndAttendaceAsync(Expression<Func<GradeAndAttendace, bool>>? func = null,
             Expression<Func<GradeAndAttendace, object>>? orderBy = null,
             bool isOrderByDesting = false,
            params Expression<Func<GradeAndAttendace, object>>[] includes);
        Task<GradeAndAttendace> GetGradeAndAttendaceAsync(Func<GradeAndAttendace, bool>? func = null);
        Task<bool> CheckGradeAndAttendaceAsync(string studentId, int lessonId);
        Task AddOrUpdateGradeAndAttendaceAsync(GradeAndAttendaceDto dto);
    }
}
