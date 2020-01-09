using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiRest.NetCore.Domain.Models.MongoDB;

namespace WebApiRest.NetCore.Domain.Interfaces.Repositories.MongoDB
{
    public interface IGradeRepository
    {
        Task<GradeModel> Create(GradeModel obj);

        Task<GradeModel> Read(string grade_id);

        Task<IEnumerable<GradeModel>> Read(int top);

        Task<IEnumerable<GradeModel>> Read();

        Task Update(GradeModel obj);

        Task Delete(string grade_id);

        Task Delete(string[] grade_ids);
    }
}