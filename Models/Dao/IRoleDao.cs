using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiRest.NetCore.Models.Dtos;

namespace WebApiRest.NetCore.Models.Dao
{
    public interface IRoleDao
    {
        Task<RoleDto> Create(RoleDto obj);

        Task<RoleDto> Read(int id);

        Task<IEnumerable<RoleDto>> Read();

        Task Update(RoleDto obj);

        Task Delete(int id);
    }
}