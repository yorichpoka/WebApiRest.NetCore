using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiRest.NetCore.Domain.Models.MongoDB;

namespace WebApiRest.NetCore.Domain.Interfaces.Repositories.MongoDB
{
    public interface IAddressRepository
    {
        Task<AddressModel> Create(AddressModel obj);

        Task<AddressModel> Read(string address_id);

        Task<IEnumerable<AddressModel>> Read(int top);

        Task<IEnumerable<AddressModel>> Read();

        Task Update(AddressModel obj);

        Task Delete(string address_id);

        Task Delete(string[] address_ids);
    }
}