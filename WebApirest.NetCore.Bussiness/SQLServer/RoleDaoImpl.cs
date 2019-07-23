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
    public class RoleDaoImpl : IRoleDao
    {
        private readonly DataBaseSQLServerContext _DataBaseSQLServerContext;

        public RoleDaoImpl(DataBaseSQLServerContext dataBaseSQLServerContext)
        {
            this._DataBaseSQLServerContext = dataBaseSQLServerContext;
        }

        public Task<RoleModel> Create(RoleModel obj)
        {
            return
                Task.Factory.StartNew<RoleModel>(() =>
                {
                    var value = MapperSingleton.Instance.Map<RoleModel, Role>(obj);

                    this._DataBaseSQLServerContext.Roles.Add(value);

                    this._DataBaseSQLServerContext.SaveChanges();

                    return MapperSingleton.Instance.Map<Role, RoleModel>(value);
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

                    return MapperSingleton.Instance.Map<Role, RoleModel>(value.Result);
                });
        }

        public Task<IEnumerable<RoleModel>> Read()
        {
            return
                Task.Factory.StartNew<IEnumerable<RoleModel>>(() =>
                {
                    var value = this._DataBaseSQLServerContext.Roles.ToList();

                    return MapperSingleton.Instance.Map<IEnumerable<Role>, IEnumerable<RoleModel>>(value);
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