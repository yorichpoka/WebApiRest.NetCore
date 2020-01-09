using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiRest.NetCore.Domain.Models;

namespace WebApiRest.NetCore.Domain.Interfaces.Bussiness
{
    public interface IAuthorizationBussiness
    {
        Task<AuthorizationModel> Create(AuthorizationModel obj);

        Task<AuthorizationModel> Read(int idRole, int idMenu);

        Task<IEnumerable<AuthorizationModel>> Read();

        Task Update(AuthorizationModel obj);

        Task Delete(int idRole, int idMenu);

        Task Delete(int[][] ids);
    }
}