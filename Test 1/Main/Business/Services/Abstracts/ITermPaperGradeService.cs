using Business.DTOs.Student.Files;
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
    public interface ITermPaperGradeService
    {
        Task CreateTermPaperGrade(TermPaperGradeDto fileDto);
        Task DeleteTermPaperGrade(int id);
        Task SoftDeleteTermPaperGrade(int id);
        Task<TermPaperGrade> GetTermPaperGrade(Func<TermPaperGrade, bool>? func = null);
        Task<List<TermPaperGrade>> GetAllTermPapers
            (
            Expression<Func<TermPaperGrade, bool>>? func = null,
            Expression<Func<TermPaperGrade, object>>? orderBy = null,
            bool isOrderByDesting = false,
            params Expression<Func<TermPaperGrade, object>>[] includes
            );
        Task UpdateTermPaperGrade(int id, TermPaperGradeDto newTermPaper);
        Task CreateOrUpdateTermPaperGrade(TermPaperGradeDto termPaperGradeDto);
    }
}
