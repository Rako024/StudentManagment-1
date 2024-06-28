using Business.DTOs.EntryPoints;
using Business.DTOs.Student.AcademicResult;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Helper
{
    public static class EntryPointCalculator
    {
        public static EntryPointResultDto Calculator(EntryPointCalculatorDto dto)
        {
            int average = 0;
            if(!dto.Grades.IsNullOrEmpty())
            {
                int sum = 0;
                foreach(var grade in dto.Grades)
                {
                    sum += grade;
                }
                average = sum/dto.Grades.Count;
            }
            int col1 = 0;
            int col2 = 0;
            int col3 = 0;
            if(dto.ColloquiumFirst!=null)
            {
                col1 = (int)dto.ColloquiumFirst;
            }
            if (dto.ColloquiumSecound != null)
            {
                col2 = (int)dto.ColloquiumSecound;
            }
            if (dto.ColloquiumThird != null)
            {
                col3 = (int)dto.ColloquiumThird;
            }
            int colAvarage = ((col1 + col2 + col3)/3)*2;
            int termGrade = 0;
            if(dto.TermPaperGrade != null)
            {
                termGrade = (int)dto.TermPaperGrade;
            }
            int qbLimitCount = dto.Lesson.LessonCount/4;
            int qbCout = dto.QbCount;
            bool isFailed = false;
            if(qbLimitCount < qbCout) 
            {
                isFailed = true;
            }
            int attendancePoint = 10;
            if(qbCout > 0)
            {
                if(qbCout >= 4)
                {
                    attendancePoint -= 2;
                }else if(qbCout >= 2)
                {
                    attendancePoint -= 1;
                }
            }
            EntryPointResultDto result = new EntryPointResultDto()
            {
                StudentUserId = dto.StudentUserId,
                Lesson = dto.Lesson,
                GradeAverage = average,
                ColloquiumPoint = colAvarage,
                TermPoint = termGrade,
                AttendancePoint = attendancePoint,
                TotalPoint = average + colAvarage + termGrade + attendancePoint,
                IsFailed = isFailed,
            };
            return result;
        }

        public static int SemesterGPA(List<StudentEntryPointAndExamScoreDto> dtoList)
        {
            if(dtoList.IsNullOrEmpty())
            {
                return 0;
            }
            int sumPoint = 0;
            int creditSum = 0;
            foreach(StudentEntryPointAndExamScoreDto dto in dtoList)
            {
                int credit = dto.Lesson.Credit;
                creditSum += credit;
                if (dto.ExamScore.Score != null)
                {
                     sumPoint += (dto.EntryPoint.TotalPoint + (int)dto.ExamScore.Score) * credit;
                }
                else
                {
                    sumPoint = dto.EntryPoint.TotalPoint * credit;
                }
            
            }
            return sumPoint/creditSum;
        }
    }
}
