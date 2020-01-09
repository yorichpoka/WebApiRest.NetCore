using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiRest.NetCore.Domain.Models;

namespace WebApiRest.NetCore.Domain.Interfaces.Bussiness
{
    public interface IUserBussiness
    {
        Task<UserModel> Create(UserModel obj);
        Task<UserModel> Read(int id);
        Task<UserModel> Read(string login, string password);
        Task<IEnumerable<UserModel>> Read();
        Task<IEnumerable<UserRoleModel>> ReadWithRoles();
        Task<UserModel> Update(UserModel obj);
        Task Delete(int id);
        Task Delete(int[] ids);
        void InitHubConnectionId(int id);
        void SetHubConnectionId(string hubConnectionId, int id);
        string GetHubConnectionId(int id);
        Task<bool> RemoveHubConnectionId(string hubConnectionId, int? id);
        bool IsUserConnected(int id);
        bool IsUserConnected(int[] id);
    }
}