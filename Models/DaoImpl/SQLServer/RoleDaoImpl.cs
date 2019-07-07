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
    public class RoleDaoImpl : IRoleDao
    {
        private readonly DataBaseSQLServerContext _DataBaseSQLServerContext;

        public RoleDaoImpl(DataBaseSQLServerContext dataBaseSQLServerContext)
        {
            this._DataBaseSQLServerContext = dataBaseSQLServerContext;
        }

        public Task<RoleDto> Create(RoleDto obj)
        {
            return
                Task.Factory.StartNew<RoleDto>(() =>
                {
                    var value = MapperSingleton.Instance.Map<RoleDto, Role>(obj);

                    this._DataBaseSQLServerContext.Roles.Add(value);

                    this._DataBaseSQLServerContext.SaveChanges();

                    return MapperSingleton.Instance.Map<Role, RoleDto>(value);
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

        public Task<RoleDto> Read(int id)
        {
            return
                Task.Factory.StartNew<RoleDto>(() =>
                {
                    var value = this._DataBaseSQLServerContext.Roles.FindAsync(id);

                    return MapperSingleton.Instance.Map<Role, RoleDto>(value.Result);
                });
        }

        public Task<IEnumerable<RoleDto>> Read()
        {
            return
                Task.Factory.StartNew<IEnumerable<RoleDto>>(() =>
                {
                    var value = this._DataBaseSQLServerContext.Roles.ToList();

                    return MapperSingleton.Instance.Map<IEnumerable<Role>, IEnumerable<RoleDto>>(value);
                });
        }

        public Task Update(RoleDto obj)
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