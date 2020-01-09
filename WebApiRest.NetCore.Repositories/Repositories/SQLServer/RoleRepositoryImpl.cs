using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiRest.NetCore.Domain.Interfaces.Repositories;
using WebApiRest.NetCore.Domain.Models;
using WebApiRest.NetCore.Repositories.Contexts;
using SqlServerPkg = WebApiRest.NetCore.Repositories.Entities.SqlServer;

namespace WebApiRest.NetCore.Repositories.Repositories.SqlServer
{
    public class RoleRepositoryImpl : IRoleRepository
    {
        private readonly DataBaseSQLServerContext _DataBaseSQLServerContext;
        private readonly IMapper _Mapper;

        public RoleRepositoryImpl(DataBaseSQLServerContext dataBaseSQLServerContext, IMapper mapper)
        {
            this._DataBaseSQLServerContext = dataBaseSQLServerContext;
            this._Mapper = mapper;
        }

        public Task<RoleModel> Create(RoleModel obj)
        {
            return
                Task.Factory.StartNew<RoleModel>(() =>
                {
                    var value = this._Mapper.Map<RoleModel, SqlServerPkg.Role>(obj);

                    this._DataBaseSQLServerContext.Roles.Add(value);

                    this._DataBaseSQLServerContext.SaveChanges();

                    return this._Mapper.Map<SqlServerPkg.Role, RoleModel>(value);
                });
        }

        public Task Delete(int id)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    this._DataBaseSQLServerContext.Roles.Remove(
                        this._DataBaseSQLServerContext.Roles.Find(id)
                    );

                    this._DataBaseSQLServerContext.SaveChanges();
                });
        }

        public Task Delete(int[] ids)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    this._DataBaseSQLServerContext.Roles.RemoveRange(
                        this._DataBaseSQLServerContext.Roles.Where(l => ids.Contains(l.Id))
                    );

                    this._DataBaseSQLServerContext.SaveChanges();
                });
        }

        public Task<RoleModel> Read(int id)
        {
            return
                Task.Factory.StartNew<RoleModel>(() =>
                {
                    var value = this._DataBaseSQLServerContext.Roles.Include(l => l.Users)
                                                                    .FirstOrDefault(l => l.Id == id);

                    return this._Mapper.Map<SqlServerPkg.Role, RoleModel>(value);
                });
        }

        public Task<IEnumerable<RoleModel>> Read()
        {
            return
                Task.Factory.StartNew<IEnumerable<RoleModel>>(() =>
                {
                    var value = this._DataBaseSQLServerContext.Roles.Include(l => l.Users)
                                                                    .ToList();

                    return this._Mapper.Map<IEnumerable<SqlServerPkg.Role>, IEnumerable<RoleModel>>(value);
                });
        }

        public Task Update(RoleModel obj)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    var value = this._DataBaseSQLServerContext.Roles.Find(obj.Id);

                    value.ExtUpdate(obj);

                    this._DataBaseSQLServerContext.Roles.Update(value);

                    this._DataBaseSQLServerContext.SaveChanges();
                });
        }
    }
}