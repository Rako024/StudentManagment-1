using Business.DTOs.EntryPoints;
using Core.Models;
using System;
using System.Collections.Generic;

namespace Business.DTOs.Student.AcademicResult
{
    public record AcademicResultsEntryPointAndExamScoreDto
    {
        public StudentUser StudentUser { get; set; }
        public List<StudentEntryPointAndExamScoreDto> Year1Semester1 { get; set; } = new List<StudentEntryPointAndExamScoreDto>();
        public int SemesterGPA11 { get; set; }
        public List<StudentEntryPointAndExamScoreDto> Year1Semester2 { get; set; } = new List<StudentEntryPointAndExamScoreDto>();
        public int SemesterGPA12 { get; set; }

        public List<StudentEntryPointAndExamScoreDto> Year1Semester3 { get; set; } = new List<StudentEntryPointAndExamScoreDto>();
        public int SemesterGPA13 { get; set; }


        public List<StudentEntryPointAndExamScoreDto> Year2Semester1 { get; set; } = new List<StudentEntryPointAndExamScoreDto>();
        public int SemesterGPA21 { get; set; }

        public List<StudentEntryPointAndExamScoreDto> Year2Semester2 { get; set; } = new List<StudentEntryPointAndExamScoreDto>();
        public int SemesterGPA22 { get; set; }

        public List<StudentEntryPointAndExamScoreDto> Year2Semester3 { get; set; } = new List<StudentEntryPointAndExamScoreDto>();
        public int SemesterGPA23 { get; set; }


        public List<StudentEntryPointAndExamScoreDto> Year3Semester1 { get; set; } = new List<StudentEntryPointAndExamScoreDto>();
        public int SemesterGPA31 { get; set; }

        public List<StudentEntryPointAndExamScoreDto> Year3Semester2 { get; set; } = new List<StudentEntryPointAndExamScoreDto>();
        public int SemesterGPA32 { get; set; }

        public List<StudentEntryPointAndExamScoreDto> Year3Semester3 { get; set; } = new List<StudentEntryPointAndExamScoreDto>();
        public int SemesterGPA33 { get; set; }


        public List<StudentEntryPointAndExamScoreDto> Year4Semester1 { get; set; } = new List<StudentEntryPointAndExamScoreDto>();
        public int SemesterGPA41 { get; set; }

        public List<StudentEntryPointAndExamScoreDto> Year4Semester2 { get; set; } = new List<StudentEntryPointAndExamScoreDto>();
        public int SemesterGPA42 { get; set; }

        public List<StudentEntryPointAndExamScoreDto> Year4Semester3 { get; set; } = new List<StudentEntryPointAndExamScoreDto>();
        public int SemesterGPA43 { get; set; }
    }
}
