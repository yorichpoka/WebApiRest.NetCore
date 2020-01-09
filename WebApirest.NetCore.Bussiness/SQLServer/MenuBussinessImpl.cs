using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiRest.NetCore.Domain.Interfaces.Bussiness;
using WebApiRest.NetCore.Domain.Interfaces.Repositories;
using WebApiRest.NetCore.Domain.Models;

namespace WebApiRest.NetCore.Bussiness.SqlServer
{
    public class MenuBussinessImpl : IMenuBussiness
    {
        private readonly IMenuRepository _MenuRepository;

        public MenuBussinessImpl(IMenuRepository repository)
        {
            this._MenuRepository = repository;
        }

        public Task<MenuModel> Create(MenuModel obj)
        {
            return
                this._MenuRepository.Create(obj);
        }

        public Task Delete(int id)
        {
            return
                this._MenuRepository.Delete(id);
        }

        public Task Delete(int[] ids)
        {
            return
                this._MenuRepository.Delete(ids);
        }

        public Task<MenuModel> Read(int id)
        {
            return
                this._MenuRepository.Read(id);
        }

        public Task<IEnumerable<MenuModel>> Read()
        {
            return
                this._MenuRepository.Read();
        }

        public Task Update(MenuModel obj)
        {
            return
                this._MenuRepository.Update(obj);
        }
    }
}