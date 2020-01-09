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
    public class RoleRepositoryImpl : IRoleRepository
    {
        private readonly DataBaseMySQLContext _DataBaseMySQLContext;
        private readonly IMapper _Mapper;

        public RoleRepositoryImpl(DataBaseMySQLContext dataBaseMySQLContext, IMapper mapper)
        {
            this._DataBaseMySQLContext = dataBaseMySQLContext;
            this._Mapper = mapper;
        }

        public Task<RoleModel> Create(RoleModel obj)
        {
            return
                Task.Factory.StartNew<RoleModel>(() =>
                {
                    var value = this._Mapper.Map<RoleModel, MySqlPkg.Role>(obj);

                    this._DataBaseMySQLContext.Roles.Add(value);

                    this._DataBaseMySQLContext.SaveChanges();

                    return this._Mapper.Map<MySqlPkg.Role, RoleModel>(value);
                });
        }

        public Task Delete(int id)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    this._DataBaseMySQLContext.Roles.Remove(
                        this._DataBaseMySQLContext.Roles.Find(id)
                    );

                    this._DataBaseMySQLContext.SaveChanges();
                });
        }

        public Task Delete(int[] ids)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    this._DataBaseMySQLContext.Roles.RemoveRange(
                        this._DataBaseMySQLContext.Roles.Where(l => ids.Contains(l.Id))
                    );

                    this._DataBaseMySQLContext.SaveChanges();
                });
        }

        public Task<RoleModel> Read(int id)
        {
            return
                Task.Factory.StartNew<RoleModel>(() =>
                {
                    var value = this._DataBaseMySQLContext.Roles.Include(l => l.Users)
                                                                .FirstOrDefault(l => l.Id == id);

                    return this._Mapper.Map<MySqlPkg.Role, RoleModel>(value);
                });
        }

        public Task<IEnumerable<RoleModel>> Read()
        {
            return
                Task.Factory.StartNew<IEnumerable<RoleModel>>(() =>
                {
                    var value = this._DataBaseMySQLContext.Roles.Include(l => l.Users)
                                                                .ToList();

                    return this._Mapper.Map<IEnumerable<MySqlPkg.Role>, IEnumerable<RoleModel>>(value);
                });
        }

        public Task Update(RoleModel obj)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    var value = this._DataBaseMySQLContext.Roles.Find(obj.Id);

                    value.ExtUpdate(obj);

                    this._DataBaseMySQLContext.Roles.Update(value);

                    this._DataBaseMySQLContext.SaveChanges();
                });
        }
    }
}