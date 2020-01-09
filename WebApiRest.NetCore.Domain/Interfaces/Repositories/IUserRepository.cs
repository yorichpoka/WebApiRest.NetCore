using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiRest.NetCore.Domain.Models;

namespace WebApiRest.NetCore.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<UserModel> Create(UserModel obj);

        Task<UserModel> Read(int id);

        Task<UserModel> Read(string login, string password);

        Task<IEnumerable<UserModel>> Read();

        Task<IEnumerable<UserRoleModel>> ReadWithRoles();

        Task<UserModel> Update(UserModel obj);

        Task Delete(int id);

        Task Delete(int[] ids);
    }
}