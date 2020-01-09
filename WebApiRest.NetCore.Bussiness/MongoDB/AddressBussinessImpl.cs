using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiRest.NetCore.Domain.Interfaces.Bussiness.MongoDB;
using WebApiRest.NetCore.Domain.Interfaces.Repositories.MongoDB;
using WebApiRest.NetCore.Domain.Models.MongoDB;

namespace WebApiRest.NetCore.Bussiness.MySql
{
    public class AddressBussinessImpl : IAddressBussiness
    {
        private readonly IAddressRepository _AddressRepository;

        public AddressBussinessImpl(IAddressRepository repository)
        {
            this._AddressRepository = repository;
        }

        public Task<AddressModel> Create(AddressModel obj)
        {
            return
                this._AddressRepository.Create(obj);
        }

        public Task Delete(string address_id)
        {
            return
                this._AddressRepository.Delete(address_id);
        }

        public Task Delete(string[] address_ids)
        {
            return
                this._AddressRepository.Delete(address_ids);
        }

        public Task<AddressModel> Read(string id)
        {
            return
                this._AddressRepository.Read(id);
        }

        public Task<IEnumerable<AddressModel>> Read(int top)
        {
            return
                this._AddressRepository.Read(top);
        }

        public Task<IEnumerable<AddressModel>> Read()
        {
            return
                this._AddressRepository.Read();
        }

        public Task Update(AddressModel obj)
        {
            return
                this._AddressRepository.Update(obj);
        }
    }
}