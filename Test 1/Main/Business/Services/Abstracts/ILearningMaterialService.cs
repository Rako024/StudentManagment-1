using Business.DTOs.Teacher.Homeworks;
using Business.DTOs.Teacher.LearningMaterials;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstracts
{
    public interface ILearningMaterialService
    {
        Task CreateLearningMaterial(CreateLearningMaterialDto dto);
        Task UpdateLearningMaterial(int id, CreateLearningMaterialDto dto);
        Task DeleteLearningMaterial(int id);
        Task<LearningMaterial> GetLearningMaterial(Expression<Func<LearningMaterial, bool>>? func = null);
        Task<List<LearningMaterial>> GetAllLearningMaterials(Expression<Func<LearningMaterial, bool>>? func = null, Expression<Func<LearningMaterial, object>>? orderBy = null, bool isOrderByDescending = false, params Expression<Func<LearningMaterial, object>>[] includes);
    
    }
}
