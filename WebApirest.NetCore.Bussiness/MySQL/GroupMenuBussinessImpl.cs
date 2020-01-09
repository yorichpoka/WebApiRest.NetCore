using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiRest.NetCore.Domain.Interfaces.Bussiness;
using WebApiRest.NetCore.Domain.Interfaces.Repositories;
using WebApiRest.NetCore.Domain.Models;

namespace WebApiRest.NetCore.Bussiness.MySql
{
    public class GroupMenuBussinessImpl : IGroupMenuBussiness
    {
        private readonly IGroupMenuRepository _GroupMenuRepository;

        public GroupMenuBussinessImpl(IGroupMenuRepository repository)
        {
            this._GroupMenuRepository = repository;
        }

        public Task<GroupMenuModel> Create(GroupMenuModel obj)
        {
            return
                this._GroupMenuRepository.Create(obj);
        }

        public Task Delete(int id)
        {
            return
                this._GroupMenuRepository.Delete(id);
        }

        public Task Delete(int[] ids)
        {
            return
                this._GroupMenuRepository.Delete(ids);
        }

        public Task<GroupMenuModel> Read(int id)
        {
            return
                this._GroupMenuRepository.Read(id);
        }

        public Task<IEnumerable<GroupMenuModel>> Read()
        {
            return
                this._GroupMenuRepository.Read();
        }

        public Task Update(GroupMenuModel obj)
        {
            return
                this._GroupMenuRepository.Update(obj);
        }
    }
}