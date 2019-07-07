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
        public Task<RoleDto> Create(RoleDto obj)
        {
            return
                Task.Factory.StartNew<RoleDto>(() =>
                {
                    using (var db = new TestDBMySQLEntities())
                    {
                        var value = MapperSingleton.Instance.Map<RoleDto, TblRole>(obj);

                        db.Roles.Add(value);

                        db.SaveChanges();

                        return MapperSingleton.Instance.Map<TblRole, RoleDto>(value);
                    }
                });
        }

        public Task Delete(int id)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    using (var db = new TestDBMySQLEntities())
                    {
                        db.Roles.Remove(
                            db.Roles.Find(id)
                        );

                        db.SaveChanges();
                    }
                });
        }

        public Task<RoleDto> Read(int id)
        {
            return
                Task.Factory.StartNew<RoleDto>(() =>
                {
                    using (var db = new TestDBMySQLEntities())
                    {
                        var value = db.Roles.FindAsync(id);

                        return MapperSingleton.Instance.Map<TblRole, RoleDto>(value.Result);
                    }
                });
        }

        public Task<IEnumerable<RoleDto>> Read()
        {
            return
                Task.Factory.StartNew<IEnumerable<RoleDto>>(() =>
                {
                    using (var db = new TestDBMySQLEntities())
                    {
                        var value = db.Roles.ToList();

                        return MapperSingleton.Instance.Map<IEnumerable<TblRole>, IEnumerable<RoleDto>>(value);
                    }
                });
        }

        public Task Update(RoleDto obj)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    using (var db = new TestDBMySQLEntities())
                    {
                        var value = db.Roles.Find(obj.Id);

                        value.ExtUpdate(obj);

                        db.Roles.Update(value);

                        db.SaveChanges();
                    }
                });
        }
    }
}