using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiRest.NetCore.Models.Dtos;

namespace WebApiRest.NetCore.Models.Dao
{
    public interface IUserDao
    {
        Task<UserDto> Create(UserDto obj);

        Task<UserDto> Read(int id);

        Task<UserDto> Read(string login, string password);

        Task<IEnumerable<UserDto>> Read();

        Task Update(UserDto obj);

        Task Delete(int id);
    }
}