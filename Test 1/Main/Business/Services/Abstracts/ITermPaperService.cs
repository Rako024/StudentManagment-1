using Business.DTOs.Student.Files;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstracts
{
    public interface ITermPaperService
    {
        Task CreateTermPaper(TermFileDto fileDto);
        Task DeleteTermPaper(int id);
        Task SoftDeleteTermPaper(int id);
        Task<TermPaper> GetTermPaper(Func<TermPaper, bool>? func = null);
        Task<List<TermPaper>> GetAllTermPapers
            (
            Expression<Func<TermPaper, bool>>? func = null,
            Expression<Func<TermPaper, object>>? orderBy = null,
            bool isOrderByDesting = false,
            params Expression<Func<TermPaper, object>>[] includes
            );
        Task UpdateTermPaper(int id,TermFileUpdateDto newTermPaper);
    }
}
