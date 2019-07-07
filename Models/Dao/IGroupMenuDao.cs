using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiRest.NetCore.Models.Dtos;

namespace WebApiRest.NetCore.Models.Dao
{
    public interface IGroupMenuDao
    {
        Task<GroupMenuDto> Create(GroupMenuDto obj);

        Task<GroupMenuDto> Read(int id);

        Task<IEnumerable<GroupMenuDto>> Read();

        Task Update(GroupMenuDto obj);

        Task Delete(int id);
    }
}