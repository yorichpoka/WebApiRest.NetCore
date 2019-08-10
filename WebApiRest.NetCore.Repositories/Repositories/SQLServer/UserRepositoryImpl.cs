using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiRest.NetCore.Domain.Interfaces.Repositories;
using WebApiRest.NetCore.Domain.Models;
using WebApiRest.NetCore.Repositories;
using WebApiRest.NetCore.Repositories.Contexts;
using WebApiRest.NetCore.Repositories.Entities.SQLServer;

namespace WebApirest.NetCore.Repositories.SQLServer
{
    public class UserRepositoryImpl : IUserRepository
    {
        private readonly DataBaseSQLServerContext _DataBaseSQLServerContext;
        private readonly IMapper _Mapper;

        public UserRepositoryImpl(DataBaseSQLServerContext dataBaseSQLServerContext, IMapper mapper)
        {
            this._DataBaseSQLServerContext = dataBaseSQLServerContext;
            this._Mapper = mapper;
        }

        public Task<UserModel> Create(UserModel obj)
        {
            return
                Task.Factory.StartNew<UserModel>(() =>
                {
                    var value = this._Mapper.Map<UserModel, User>(obj);

                    this._DataBaseSQLServerContext.Users.Add(value);

                    this._DataBaseSQLServerContext.SaveChanges();

                    return this._Mapper.Map<User, UserModel>(value);
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

                    return this._Mapper.Map<User, UserModel>(value.Result);
                });
        }

        public Task<UserModel> Read(string login, string password)
        {
            return
                Task.Factory.StartNew<UserModel>(() =>
                {
                    var value = this._DataBaseSQLServerContext.Users.FirstOrDefaultAsync(l => l.Login == login && l.Password == password);

                    return this._Mapper.Map<User, UserModel>(value.Result);
                });
        }

        public Task<IEnumerable<UserModel>> Read()
        {
            return
                Task.Factory.StartNew<IEnumerable<UserModel>>(() =>
                {
                    var value = this._DataBaseSQLServerContext.Users.ToList();

                    return this._Mapper.Map<IEnumerable<User>, IEnumerable<UserModel>>(value);
                });
        }

        public Task<IEnumerable<UserRoleModel>> ReadWithRoles()
        {
            return
                Task.Factory.StartNew<IEnumerable<UserRoleModel>>(() =>
                {
                    var value = this._DataBaseSQLServerContext.Users.Include(l => l.Role).ToList();

                    return this._Mapper.Map<IEnumerable<User>, IEnumerable<UserRoleModel>>(value);
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