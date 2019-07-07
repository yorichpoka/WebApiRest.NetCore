using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiRest.NetCore.Models.Dao;
using WebApiRest.NetCore.Models.Dtos;
using WebApiRest.NetCore.Models.Entity;
using WebApiRest.NetCore.Models.Entity.SQLServer;
using WebApiRest.NetCore.Models.Tools;

namespace WebApiRest.NetCore.Models.DaoImpl.SQLServer
{
    public class UserDaoImpl : IUserDao
    {
        private readonly DataBaseSQLServerContext _DataBaseSQLServerContext;

        public UserDaoImpl(DataBaseSQLServerContext dataBaseSQLServerContext)
        {
            this._DataBaseSQLServerContext = dataBaseSQLServerContext;
        }

        public Task<UserDto> Create(UserDto obj)
        {
            return
                Task.Factory.StartNew<UserDto>(() =>
                {
                    var value = MapperSingleton.Instance.Map<UserDto, User>(obj);

                    this._DataBaseSQLServerContext.Users.Add(value);

                    this._DataBaseSQLServerContext.SaveChanges();

                    return MapperSingleton.Instance.Map<User, UserDto>(value);
                });
        }

        public Task Delete(int id)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    this._DataBaseSQLServerContext.Users.Remove(
                        this._DataBaseSQLServerContext.Users.Find(id)
                    );

                    this._DataBaseSQLServerContext.SaveChanges();
                });
        }

        public Task<UserDto> Read(int id)
        {
            return
                Task.Factory.StartNew<UserDto>(() =>
                {
                    var value = this._DataBaseSQLServerContext.Users.FindAsync(id);

                    return MapperSingleton.Instance.Map<User, UserDto>(value.Result);
                });
        }

        public Task<UserDto> Read(string login, string password)
        {
            return
                Task.Factory.StartNew<UserDto>(() =>
                {
                    var value = this._DataBaseSQLServerContext.Users.FirstOrDefaultAsync(l => l.Login == login && l.Password == password);

                    return MapperSingleton.Instance.Map<User, UserDto>(value.Result);
                });
        }

        public Task<IEnumerable<UserDto>> Read()
        {
            return
                Task.Factory.StartNew<IEnumerable<UserDto>>(() =>
                {
                    var value = this._DataBaseSQLServerContext.Users.ToList();

                    return MapperSingleton.Instance.Map<IEnumerable<User>, IEnumerable<UserDto>>(value);
                });
        }

        public Task Update(UserDto obj)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    var value = this._DataBaseSQLServerContext.Users.Find(obj.Id);

                    value.ExtUpdate(obj);

                    this._DataBaseSQLServerContext.Users.Update(value);

                    this._DataBaseSQLServerContext.SaveChanges();
                });
        }
    }
}