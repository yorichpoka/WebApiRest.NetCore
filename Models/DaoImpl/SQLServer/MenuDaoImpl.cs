using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiRest.NetCore.Models.Dao;
using WebApiRest.NetCore.Models.Dtos;
using WebApiRest.NetCore.Models.Entity;
using WebApiRest.NetCore.Models.Tools;
using WebApiRest.NetCore.Models.Entity.SQLServer;

namespace WebApiRest.NetCore.Models.DaoImpl.SQLServer
{
    public class MenuDaoImpl : IMenuDao
    {
        public Task<MenuDto> Create(MenuDto obj)
        {
            return
                Task.Factory.StartNew<MenuDto>(() =>
                {
                    using (var db = new TestDBEntities())
                    {
                        var value = MapperSingleton.Instance.Map<MenuDto, Menu>(obj);

                        db.Menus.Add(value);

                        db.SaveChanges();

                        return MapperSingleton.Instance.Map<Menu, MenuDto>(value);
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
                        db.Menus.Remove(
                            db.Menus.Find(id)
                        );

                        db.SaveChanges();
                    }
                });
        }

        public Task<MenuDto> Read(int id)
        {
            return
                Task.Factory.StartNew<MenuDto>(() =>
                {
                    using (var db = new TestDBEntities())
                    {
                        var value = db.Menus.FindAsync(id);

                        return MapperSingleton.Instance.Map<Menu, MenuDto>(value.Result);
                    }
                });
        }

        public Task<IEnumerable<MenuDto>> Read()
        {
            return
                Task.Factory.StartNew<IEnumerable<MenuDto>>(() =>
                {
                    using (var db = new TestDBEntities())
                    {
                        var value = db.Menus.ToList();

                        return MapperSingleton.Instance.Map<IEnumerable<Menu>, IEnumerable<MenuDto>>(value);
                    }
                });
        }

        public Task Update(MenuDto obj)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    using (var db = new TestDBEntities())
                    {
                        var value = db.Menus.Find(obj.Id);

                        value.ExtUpdate(obj);

                        db.Menus.Update(value);

                        db.SaveChanges();
                    }
                });
        }
    }
}