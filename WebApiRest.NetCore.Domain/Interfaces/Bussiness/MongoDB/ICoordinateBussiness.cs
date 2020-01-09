using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiRest.NetCore.Domain.Models.MongoDB;

namespace WebApiRest.NetCore.Domain.Interfaces.Bussiness.MongoDB
{
    public interface ICoordinateBussiness
    {
        Task<CoordinateModel> Create(CoordinateModel obj);
        Task<CoordinateModel> Read(string grade_id);
        Task<IEnumerable<CoordinateModel>> Read(int top);
        Task<IEnumerable<CoordinateModel>> Read();
        Task Update(CoordinateModel obj);
        Task Delete(string grade_id);
        Task Delete(string[] grade_ids);
    }
}