using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiRest.NetCore.Models.Dtos;

namespace WebApiRest.NetCore.Models.Dao
{
    public interface IMenuDao
    {
        Task<MenuDto> Create(MenuDto obj);

        Task<MenuDto> Read(int id);

        Task<IEnumerable<MenuDto>> Read();

        Task Update(MenuDto obj);

        Task Delete(int id);
    }
}