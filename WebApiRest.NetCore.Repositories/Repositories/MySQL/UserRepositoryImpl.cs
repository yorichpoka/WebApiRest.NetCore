using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiRest.NetCore.Domain.Interfaces.Repositories;
using WebApiRest.NetCore.Domain.Models;
using WebApiRest.NetCore.Repositories.Contexts;
using MySqlPkg = WebApiRest.NetCore.Repositories.Entities.MySql;

namespace WebApiRest.NetCore.Repositories.Repositories.MySql
{
    public class UserRepositoryImpl : IUserRepository
    {
        private readonly DataBaseMySQLContext _DataBaseMySQLContext;
        private readonly IMapper _Mapper;

        public UserRepositoryImpl(DataBaseMySQLContext dataBaseMySQLContext, IMapper mapper)
        {
            this._DataBaseMySQLContext = dataBaseMySQLContext;
            this._Mapper = mapper;
        }

        public Task<UserModel> Create(UserModel obj)
        {
            return
                Task.Factory.StartNew<UserModel>(() =>
                {
                    var value = this._Mapper.Map<UserModel, MySqlPkg.User>(obj);

                    this._DataBaseMySQLContext.Users.Add(value);

                    this._DataBaseMySQLContext.SaveChanges();

                    return this._Mapper.Map<MySqlPkg.User, UserModel>(value);
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

        public Task Delete(int[] ids)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    this._DataBaseMySQLContext.Users.RemoveRange(
                        this._DataBaseMySQLContext.Users.Where(l => ids.Contains(l.Id))
                    );

                    this._DataBaseMySQLContext.SaveChanges();
                });
        }

        public Task<UserModel> Read(int id)
        {
            return
                Task.Factory.StartNew<UserModel>(() =>
                {
                    var value = this._DataBaseMySQLContext.Users.FindAsync(id);

                    return this._Mapper.Map<MySqlPkg.User, UserModel>(value.Result);
                });
        }

        public Task<UserModel> Read(string login, string password)
        {
            return
                Task.Factory.StartNew<UserModel>(() =>
                {
                    var value = this._DataBaseMySQLContext.Users.FirstOrDefaultAsync(l => l.Login == login && l.Password == password);

                    return this._Mapper.Map<MySqlPkg.User, UserModel>(value.Result);
                });
        }

        public Task<IEnumerable<UserModel>> Read()
        {
            return
                Task.Factory.StartNew<IEnumerable<UserModel>>(() =>
                {
                    var value = this._DataBaseMySQLContext.Users.ToList();

                    return this._Mapper.Map<IEnumerable<MySqlPkg.User>, IEnumerable<UserModel>>(value);
                });
        }

        public Task<IEnumerable<UserRoleModel>> ReadWithRoles()
        {
            return
                Task.Factory.StartNew<IEnumerable<UserRoleModel>>(() =>
                {
                    var value = this._DataBaseMySQLContext.Users.Include(l => l.Role).ToList();

                    return this._Mapper.Map<IEnumerable<MySqlPkg.User>, IEnumerable<UserRoleModel>>(value);
                });
        }

        public Task<UserModel> Update(UserModel obj)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    var value = this._DataBaseMySQLContext.Users.Find(obj.Id);

                    value.ExtUpdate(obj);

                    this._DataBaseMySQLContext.Users.Update(value);

                    this._DataBaseMySQLContext.SaveChanges();

                    return this._Mapper.Map<MySqlPkg.User, UserModel>(value);
                });
        }
    }
}