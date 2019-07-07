using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiRest.NetCore.Models.Dao;
using WebApiRest.NetCore.Models.Dtos;
using WebApiRest.NetCore.Models.Entity.MySQL;
using WebApiRest.NetCore.Models.Tools;

namespace WebApiRest.NetCore.Models.DaoImpl.MySQL
{
    public class RoleDaoImpl : IRoleDao
    {
        private readonly DataBaseMySQLContext _DataBaseMySQLContext;

        public RoleDaoImpl(DataBaseMySQLContext dataBaseMySQLContext)
        {
            this._DataBaseMySQLContext = dataBaseMySQLContext;
        }

        public Task<RoleDto> Create(RoleDto obj)
        {
            return
                Task.Factory.StartNew<RoleDto>(() =>
                {
                    var value = MapperSingleton.Instance.Map<RoleDto, TblRole>(obj);

                    this._DataBaseMySQLContext.Roles.Add(value);

                    this._DataBaseMySQLContext.SaveChanges();

                    return MapperSingleton.Instance.Map<TblRole, RoleDto>(value);
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

        public Task<RoleDto> Read(int id)
        {
            return
                Task.Factory.StartNew<RoleDto>(() =>
                {
                    var value = this._DataBaseMySQLContext.Roles.FindAsync(id);

                    return MapperSingleton.Instance.Map<TblRole, RoleDto>(value.Result);
                });
        }

        public Task<IEnumerable<RoleDto>> Read()
        {
            return
                Task.Factory.StartNew<IEnumerable<RoleDto>>(() =>
                {
                    var value = this._DataBaseMySQLContext.Roles.ToList();

                    return MapperSingleton.Instance.Map<IEnumerable<TblRole>, IEnumerable<RoleDto>>(value);
                });
        }

        public Task Update(RoleDto obj)
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