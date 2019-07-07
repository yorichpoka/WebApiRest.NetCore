using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiRest.NetCore.Models.Dao;
using WebApiRest.NetCore.Models.Dtos;
using WebApiRest.NetCore.Models.Entity.MySQL;
using WebApiRest.NetCore.Models.Tools;

namespace WebApiRest.NetCore.Models.DaoImpl.MySQL
{
    public class UserDaoImpl : IUserDao
    {
        private readonly DataBaseMySQLContext _DataBaseMySQLContext;

        public UserDaoImpl(DataBaseMySQLContext dataBaseMySQLContext)
        {
            this._DataBaseMySQLContext = dataBaseMySQLContext;
        }

        public Task<UserDto> Create(UserDto obj)
        {
            return
                Task.Factory.StartNew<UserDto>(() =>
                {
                    var value = MapperSingleton.Instance.Map<UserDto, TblUser>(obj);

                    this._DataBaseMySQLContext.Users.Add(value);

                    this._DataBaseMySQLContext.SaveChanges();

                    return MapperSingleton.Instance.Map<TblUser, UserDto>(value);
                });
        }

        public Task Delete(int id)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    this._DataBaseMySQLContext.Users.Remove(
                        this._DataBaseMySQLContext.Users.Find(id)
                    );

                    this._DataBaseMySQLContext.SaveChanges();
                });
        }

        public Task<UserDto> Read(int id)
        {
            return
                Task.Factory.StartNew<UserDto>(() =>
                {
                    var value = this._DataBaseMySQLContext.Users.FindAsync(id);

                    return MapperSingleton.Instance.Map<TblUser, UserDto>(value.Result);
                });
        }

        public Task<UserDto> Read(string login, string password)
        {
            return
                Task.Factory.StartNew<UserDto>(() =>
                {
                    var value = this._DataBaseMySQLContext.Users.FirstOrDefaultAsync(l => l.Login == login && l.Password == password);

                    return MapperSingleton.Instance.Map<TblUser, UserDto>(value.Result);
                });
        }

        public Task<IEnumerable<UserDto>> Read()
        {
            return
                Task.Factory.StartNew<IEnumerable<UserDto>>(() =>
                {
                    var value = this._DataBaseMySQLContext.Users.ToList();

                    return MapperSingleton.Instance.Map<IEnumerable<TblUser>, IEnumerable<UserDto>>(value);
                });
        }

        public Task Update(UserDto obj)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    var value = this._DataBaseMySQLContext.Users.Find(obj.Id);

                    value.ExtUpdate(obj);

                    this._DataBaseMySQLContext.Users.Update(value);

                    this._DataBaseMySQLContext.SaveChanges();
                });
        }
    }
}