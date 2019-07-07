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
        public Task<RoleDto> Create(RoleDto obj)
        {
            return
                Task.Factory.StartNew<RoleDto>(() =>
                {
                    using (var db = new TestDBEntities())
                    {
                        var value = MapperSingleton.Instance.Map<RoleDto, Role>(obj);

                        db.Roles.Add(value);

                        db.SaveChanges();

                        return MapperSingleton.Instance.Map<Role, RoleDto>(value);
                    }
                });
        }

        public Task Delete(int id)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    using (var db = new TestDBEntities())
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
                    using (var db = new TestDBEntities())
                    {
                        var value = db.Roles.FindAsync(id);

                        return MapperSingleton.Instance.Map<Role, RoleDto>(value.Result);
                    }
                });
        }

        public Task<IEnumerable<RoleDto>> Read()
        {
            return
                Task.Factory.StartNew<IEnumerable<RoleDto>>(() =>
                {
                    using (var db = new TestDBEntities())
                    {
                        var value = db.Roles.ToList();

                        return MapperSingleton.Instance.Map<IEnumerable<Role>, IEnumerable<RoleDto>>(value);
                    }
                });
        }

        public Task Update(RoleDto obj)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    using (var db = new TestDBEntities())
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