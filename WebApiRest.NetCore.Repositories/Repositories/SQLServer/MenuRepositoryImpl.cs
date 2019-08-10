using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiRest.NetCore.Domain.Interfaces.Repositories;
using WebApiRest.NetCore.Domain.Models;
using WebApiRest.NetCore.Repositories;
using WebApiRest.NetCore.Repositories.Contexts;
using WebApiRest.NetCore.Repositories.Entities.SQLServer;

namespace WebApirest.NetCore.Repositories.SQLServer
{
    public class MenuRepositoryImpl : IMenuRepository
    {
        private readonly DataBaseSQLServerContext _DataBaseSQLServerContext;
        private readonly IMapper _Mapper;

        public MenuRepositoryImpl(DataBaseSQLServerContext dataBaseSQLServerContext, IMapper mapper)
        {
            this._DataBaseSQLServerContext = dataBaseSQLServerContext;
            this._Mapper = mapper;
        }

        public Task<MenuModel> Create(MenuModel obj)
        {
            return
                Task.Factory.StartNew<MenuModel>(() =>
                {
                    var value = this._Mapper.Map<MenuModel, Menu>(obj);

                    this._DataBaseSQLServerContext.Menus.Add(value);

                    this._DataBaseSQLServerContext.SaveChanges();

                    return this._Mapper.Map<Menu, MenuModel>(value);
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

        public Task<MenuModel> Read(int id)
        {
            return
                Task.Factory.StartNew<MenuModel>(() =>
                {
                    var value = this._DataBaseSQLServerContext.Menus.FindAsync(id);

                    return this._Mapper.Map<Menu, MenuModel>(value.Result);
                });
        }

        public Task<IEnumerable<MenuModel>> Read()
        {
            return
                Task.Factory.StartNew<IEnumerable<MenuModel>>(() =>
                {
                    var value = this._DataBaseSQLServerContext.Menus.ToList();

                    return this._Mapper.Map<IEnumerable<Menu>, IEnumerable<MenuModel>>(value);
                });
        }

        public Task Update(MenuModel obj)
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