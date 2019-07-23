using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiRest.NetCore.Domain.Interfaces;
using WebApiRest.NetCore.Domain.Models;
using WebApiRest.NetCore.Repositories.Contexts;
using WebApiRest.NetCore.Repositories.Entities.SQLServer;
using WebApiRest.NetCore.Tools;

namespace WebApirest.NetCore.Bussiness.SQLServer
{
    public class UserDaoImpl : IUserDao
    {
        private readonly DataBaseSQLServerContext _DataBaseSQLServerContext;

        public UserDaoImpl(DataBaseSQLServerContext dataBaseSQLServerContext)
        {
            this._DataBaseSQLServerContext = dataBaseSQLServerContext;
        }

        public Task<UserModel> Create(UserModel obj)
        {
            return
                Task.Factory.StartNew<UserModel>(() =>
                {
                    var value = MapperSingleton.Instance.Map<UserModel, User>(obj);

                    this._DataBaseSQLServerContext.Users.Add(value);

                    this._DataBaseSQLServerContext.SaveChanges();

                    return MapperSingleton.Instance.Map<User, UserModel>(value);
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

        public Task<UserModel> Read(int id)
        {
            return
                Task.Factory.StartNew<UserModel>(() =>
                {
                    var value = this._DataBaseSQLServerContext.Users.FindAsync(id);

                    return MapperSingleton.Instance.Map<User, UserModel>(value.Result);
                });
        }

        public Task<UserModel> Read(string login, string password)
        {
            return
                Task.Factory.StartNew<UserModel>(() =>
                {
                    var value = this._DataBaseSQLServerContext.Users.FirstOrDefaultAsync(l => l.Login == login && l.Password == password);

                    return MapperSingleton.Instance.Map<User, UserModel>(value.Result);
                });
        }

        public Task<IEnumerable<UserModel>> Read()
        {
            return
                Task.Factory.StartNew<IEnumerable<UserModel>>(() =>
                {
                    var value = this._DataBaseSQLServerContext.Users.ToList();

                    return MapperSingleton.Instance.Map<IEnumerable<User>, IEnumerable<UserModel>>(value);
                });
        }

        public Task<IEnumerable<UserRoleModel>> ReadWithRoles()
        {
            return
                Task.Factory.StartNew<IEnumerable<UserRoleModel>>(() =>
                {
                    var value = this._DataBaseSQLServerContext.Users.Include(l => l.Role).ToList();

                    return MapperSingleton.Instance.Map<IEnumerable<User>, IEnumerable<UserRoleModel>>(value);
                });
        }

        public Task Update(UserModel obj)
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