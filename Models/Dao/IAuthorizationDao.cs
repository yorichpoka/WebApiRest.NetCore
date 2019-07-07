using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiRest.NetCore.Models.Dtos;

namespace WebApiRest.NetCore.Models.Dao
{
    public interface IAuthorizationDao
    {
        Task<AuthorizationDto> Create(AuthorizationDto obj);

        Task<AuthorizationDto> Read(int idRole, int idMenu);

        Task<IEnumerable<AuthorizationDto>> Read();

        Task Update(AuthorizationDto obj);

        Task Delete(int idRole, int idMenu);
    }
}