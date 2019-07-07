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
    public class GroupMenuDaoImpl : IGroupMenuDao
    {
        public Task<GroupMenuDto> Create(GroupMenuDto obj)
        {
            return
                Task.Factory.StartNew<GroupMenuDto>(() =>
                {
                    using (var db = new TestDBEntities())
                    {
                        var value = MapperSingleton.Instance.Map<GroupMenuDto, GroupMenu>(obj);

                        db.GroupMenus.Add(value);

                        db.SaveChanges();

                        return MapperSingleton.Instance.Map<GroupMenu, GroupMenuDto>(value);
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
                        db.GroupMenus.Remove(
                            db.GroupMenus.Find(id)
                        );

                        db.SaveChanges();
                    }
                });
        }

        public Task<GroupMenuDto> Read(int id)
        {
            return
                Task.Factory.StartNew<GroupMenuDto>(() =>
                {
                    using (var db = new TestDBEntities())
                    {
                        var value = db.GroupMenus.FindAsync(id);

                        return MapperSingleton.Instance.Map<GroupMenu, GroupMenuDto>(value.Result);
                    }
                });
        }

        public Task<IEnumerable<GroupMenuDto>> Read()
        {
            return
                Task.Factory.StartNew<IEnumerable<GroupMenuDto>>(() =>
                {
                    using (var db = new TestDBEntities())
                    {
                        var value = db.GroupMenus.ToList();

                        return MapperSingleton.Instance.Map<IEnumerable<GroupMenu>, IEnumerable<GroupMenuDto>>(value);
                    }
                });
        }

        public Task Update(GroupMenuDto obj)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    using (var db = new TestDBEntities())
                    {
                        var value = db.GroupMenus.Find(obj.Id);

                        value.ExtUpdate(obj);

                        db.GroupMenus.Update(value);

                        db.SaveChanges();
                    }
                });
        }
    }
}