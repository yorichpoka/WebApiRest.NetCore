using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiRest.NetCore.Domain.Interfaces.Bussiness;
using WebApiRest.NetCore.Domain.Interfaces.Repositories;
using WebApiRest.NetCore.Domain.Models;

namespace WebApiRest.NetCore.Bussiness.SqlServer
{
    public class RoleBussinessImpl : IRoleBussiness
    {
        private readonly IRoleRepository _RoleRepository;

        public RoleBussinessImpl(IRoleRepository repository)
        {
            this._RoleRepository = repository;
        }

        public Task<RoleModel> Create(RoleModel obj)
        {
            return
                this._RoleRepository.Create(obj);
        }

        public Task Delete(int id)
        {
            return
                this._RoleRepository.Delete(id);
        }

        public Task Delete(int[] ids)
        {
            return
                this._RoleRepository.Delete(ids);
        }

        public Task<RoleModel> Read(int id)
        {
            return
                this._RoleRepository.Read(id);
        }

        public Task<IEnumerable<RoleModel>> Read()
        {
            return
                this._RoleRepository.Read();
        }

        public Task Update(RoleModel obj)
        {
            return
                this._RoleRepository.Update(obj);
        }
    }
}