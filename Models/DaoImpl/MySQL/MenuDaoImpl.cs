using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiRest.NetCore.Models.Dao;
using WebApiRest.NetCore.Models.Dtos;
using WebApiRest.NetCore.Models.Entity.MySQL;
using WebApiRest.NetCore.Models.Tools;

namespace WebApiRest.NetCore.Models.DaoImpl.MySQL
{
    public class MenuDaoImpl : IMenuDao
    {
        private readonly DataBaseMySQLContext _DataBaseMySQLContext;

        public MenuDaoImpl(DataBaseMySQLContext dataBaseMySQLContext)
        {
            this._DataBaseMySQLContext = dataBaseMySQLContext;
        }

        public Task<MenuDto> Create(MenuDto obj)
        {
            return
                Task.Factory.StartNew<MenuDto>(() =>
                {
                    var value = MapperSingleton.Instance.Map<MenuDto, TblMenu>(obj);

                    this._DataBaseMySQLContext.Menus.Add(value);

                    this._DataBaseMySQLContext.SaveChanges();

                    return MapperSingleton.Instance.Map<TblMenu, MenuDto>(value);
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

        public Task<MenuDto> Read(int id)
        {
            return
                Task.Factory.StartNew<MenuDto>(() =>
                {
                    var value = this._DataBaseMySQLContext.Menus.FindAsync(id);

                    return MapperSingleton.Instance.Map<TblMenu, MenuDto>(value.Result);
                });
        }

        public Task<IEnumerable<MenuDto>> Read()
        {
            return
                Task.Factory.StartNew<IEnumerable<MenuDto>>(() =>
                {
                    var value = this._DataBaseMySQLContext.Menus.ToList();

                    return MapperSingleton.Instance.Map<IEnumerable<TblMenu>, IEnumerable<MenuDto>>(value);
                });
        }

        public Task Update(MenuDto obj)
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