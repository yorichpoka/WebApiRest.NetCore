using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiRest.NetCore.Domain.Models;

namespace WebApiRest.NetCore.Domain.Interfaces
{
    public interface IGroupMenuDao
    {
        Task<GroupMenuModel> Create(GroupMenuModel obj);

        Task<GroupMenuModel> Read(int id);

        Task<IEnumerable<GroupMenuModel>> Read();

        Task Update(GroupMenuModel obj);

        Task Delete(int id);
    }
}