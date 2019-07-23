using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiRest.NetCore.Domain.Interfaces;
using WebApiRest.NetCore.Domain.Models;
using WebApiRest.NetCore.Repositories.Contexts;
using WebApiRest.NetCore.Repositories.Entities.MySQL;
using WebApiRest.NetCore.Tools;

namespace WebApirest.NetCore.Bussiness.MySQL
{
    public class MenuDaoImpl : IMenuDao
    {
        private readonly DataBaseMySQLContext _DataBaseMySQLContext;

        public MenuDaoImpl(DataBaseMySQLContext dataBaseMySQLContext)
        {
            this._DataBaseMySQLContext = dataBaseMySQLContext;
        }

        public Task<MenuModel> Create(MenuModel obj)
        {
            return
                Task.Factory.StartNew<MenuModel>(() =>
                {
                    var value = MapperSingleton.Instance.Map<MenuModel, TblMenu>(obj);

                    this._DataBaseMySQLContext.Menus.Add(value);

                    this._DataBaseMySQLContext.SaveChanges();

                    return MapperSingleton.Instance.Map<TblMenu, MenuModel>(value);
                });
        }

        public Task Delete(int id)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    this._DataBaseMySQLContext.Menus.Remove(
                        this._DataBaseMySQLContext.Menus.Find(id)
                    );

                    this._DataBaseMySQLContext.SaveChanges();
                });
        }

        public Task<MenuModel> Read(int id)
        {
            return
                Task.Factory.StartNew<MenuModel>(() =>
                {
                    var value = this._DataBaseMySQLContext.Menus.FindAsync(id);

                    return MapperSingleton.Instance.Map<TblMenu, MenuModel>(value.Result);
                });
        }

        public Task<IEnumerable<MenuModel>> Read()
        {
            return
                Task.Factory.StartNew<IEnumerable<MenuModel>>(() =>
                {
                    var value = this._DataBaseMySQLContext.Menus.ToList();

                    return MapperSingleton.Instance.Map<IEnumerable<TblMenu>, IEnumerable<MenuModel>>(value);
                });
        }

        public Task Update(MenuModel obj)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    var value = this._DataBaseMySQLContext.Menus.Find(obj.Id);

                    value.ExtUpdate(obj);

                    this._DataBaseMySQLContext.Menus.Update(value);

                    this._DataBaseMySQLContext.SaveChanges();
                });
        }
    }
}