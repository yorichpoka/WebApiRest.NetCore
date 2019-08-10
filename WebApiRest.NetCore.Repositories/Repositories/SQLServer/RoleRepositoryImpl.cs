using AutoMapper;
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
                    var value = this._Mapper.Map<RoleModel, Role>(obj);

                    this._DataBaseSQLServerContext.Roles.Add(value);

                    this._DataBaseSQLServerContext.SaveChanges();

                    return this._Mapper.Map<Role, RoleModel>(value);
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

        public Task<RoleModel> Read(int id)
        {
            return
                Task.Factory.StartNew<RoleModel>(() =>
                {
                    var value = this._DataBaseSQLServerContext.Roles.FindAsync(id);

                    return this._Mapper.Map<Role, RoleModel>(value.Result);
                });
        }

        public Task<IEnumerable<RoleModel>> Read()
        {
            return
                Task.Factory.StartNew<IEnumerable<RoleModel>>(() =>
                {
                    var value = this._DataBaseSQLServerContext.Roles.ToList();

                    return this._Mapper.Map<IEnumerable<Role>, IEnumerable<RoleModel>>(value);
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