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
        private readonly DataBaseSQLServerContext _DataBaseSQLServerContext;

        public MenuDaoImpl(DataBaseSQLServerContext dataBaseSQLServerContext)
        {
            this._DataBaseSQLServerContext = dataBaseSQLServerContext;
        }

        public Task<MenuDto> Create(MenuDto obj)
        {
            return
                Task.Factory.StartNew<MenuDto>(() =>
                {
                    var value = MapperSingleton.Instance.Map<MenuDto, Menu>(obj);

                    this._DataBaseSQLServerContext.Menus.Add(value);

                    this._DataBaseSQLServerContext.SaveChanges();

                    return MapperSingleton.Instance.Map<Menu, MenuDto>(value);
                });
        }

        public Task Delete(int id)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    this._DataBaseSQLServerContext.Menus.Remove(
                        this._DataBaseSQLServerContext.Menus.Find(id)
                    );

                    this._DataBaseSQLServerContext.SaveChanges();
                });
        }

        public Task<MenuDto> Read(int id)
        {
            return
                Task.Factory.StartNew<MenuDto>(() =>
                {
                    var value = this._DataBaseSQLServerContext.Menus.FindAsync(id);

                    return MapperSingleton.Instance.Map<Menu, MenuDto>(value.Result);
                });
        }

        public Task<IEnumerable<MenuDto>> Read()
        {
            return
                Task.Factory.StartNew<IEnumerable<MenuDto>>(() =>
                {
                    var value = this._DataBaseSQLServerContext.Menus.ToList();

                    return MapperSingleton.Instance.Map<IEnumerable<Menu>, IEnumerable<MenuDto>>(value);
                });
        }

        public Task Update(MenuDto obj)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    var value = this._DataBaseSQLServerContext.Menus.Find(obj.Id);

                    value.ExtUpdate(obj);

                    this._DataBaseSQLServerContext.Menus.Update(value);

                    this._DataBaseSQLServerContext.SaveChanges();
                });
        }
    }
}