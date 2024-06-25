using Business.DTOs.Teacher.TeacherLessonsDto;
using Business.Services.Abstracts;
using Core.Models;
using Core.RepositoryAbstracts;
using Data.RepositoryConcretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concretes
{
    public class GradeAndAttendaceService : IGradeAndAttendaceService
    {
        IGradeAndAttendaceRepository _gradeAndAttendaceRepository;

        public GradeAndAttendaceService(IGradeAndAttendaceRepository gradeAndAttendaceRepository)
        {
            _gradeAndAttendaceRepository = gradeAndAttendaceRepository;
        }

        public async Task AddGradeAndAttendaceAsync(GradeAndAttendaceDto dto)
        {
            GradeAndAttendace gradeAndAttendace;
            if (dto.GradeAndAttendance == "i.e")
            {
                gradeAndAttendace = new GradeAndAttendace()
                {
                    StudentUserId = dto.StudentId,
                    LessonTimeId = dto.LessonTimeId,
                    IsPresent = true,
                    Score = null
                };
            }else if( dto.GradeAndAttendance == "qb")
            {
                gradeAndAttendace = new GradeAndAttendace()
                {
                    StudentUserId = dto.StudentId,
                    LessonTimeId = dto.LessonTimeId,
                    IsPresent = false,
                    Score = null
                };
            }else if(dto.GradeAndAttendance == null)
            {
                gradeAndAttendace = new GradeAndAttendace()
                {
                    StudentUserId = dto.StudentId,
                    LessonTimeId = dto.LessonTimeId,
                    IsPresent = null,
                    Score = null
                };
            }
            else
            {
                gradeAndAttendace = new GradeAndAttendace()
                {
                    StudentUserId = dto.StudentId,
                    LessonTimeId = dto.LessonTimeId,
                    IsPresent = true,
                    Score = int.Parse(dto.GradeAndAttendance)
                };
            }
            _gradeAndAttendaceRepository.Add(gradeAndAttendace);
            _gradeAndAttendaceRepository.Commit();
        }

        public async Task AddOrUpdateGradeAndAttendaceAsync(GradeAndAttendaceDto dto)
        {
            bool check = await CheckGradeAndAttendaceAsync(dto.StudentId, dto.LessonTimeId);
            if (check)
            {
                await UpdateGradeAndAttendaceAsync(dto);
            }
            else
            {
                await AddGradeAndAttendaceAsync(dto);
            }
        }

        public Task<bool> CheckGradeAndAttendaceAsync(string studentId, int lessonId)
        {
            GradeAndAttendace gradeAndAttendace = _gradeAndAttendaceRepository.Get(x=>x.StudentUserId == studentId && x.LessonTimeId == lessonId);
            if(gradeAndAttendace == null)
            {
                return Task.FromResult(false);
            }
            else
            {
                return Task.FromResult(true);
            }

        }

        public async Task<List<GradeAndAttendace>> GetAllGradeAndAttendaceAsync
            (
            Expression<Func<GradeAndAttendace, bool>>? func = null,
            Expression<Func<GradeAndAttendace, object>>? orderBy = null,
            bool isOrderByDesting = false,
            params Expression<Func<GradeAndAttendace,
                object>>[] includes
            )
        {
            var queryable = await _gradeAndAttendaceRepository.GetAll(func, orderBy, isOrderByDesting, includes);
            return queryable.ToList();
        }

        public async Task<GradeAndAttendace> GetGradeAndAttendaceAsync(Func<GradeAndAttendace, bool>? func = null)
        {
            return  _gradeAndAttendaceRepository.Get(func);
        }

        public async Task UpdateGradeAndAttendaceAsync(GradeAndAttendaceDto dto)
        {
            GradeAndAttendace gradeAndAttendace = _gradeAndAttendaceRepository.Get(x=>x.StudentUserId == dto.StudentId && x.LessonTimeId == dto.LessonTimeId);
            if (dto.GradeAndAttendance == "i.e")
            {
                gradeAndAttendace.IsPresent = true;
                gradeAndAttendace.Score = null;
            }
            else if(dto.GradeAndAttendance == "qb")
            {
                gradeAndAttendace.IsPresent = false;
                gradeAndAttendace.Score = null;
            }else if(dto.GradeAndAttendance == "~")
            {
                gradeAndAttendace.IsPresent = null;
                gradeAndAttendace.Score = null;
            }
            else
            {
                gradeAndAttendace.IsPresent= true;
                gradeAndAttendace.Score = int.Parse(dto.GradeAndAttendance);
            }

            _gradeAndAttendaceRepository.Commit();
        }
    }
}
