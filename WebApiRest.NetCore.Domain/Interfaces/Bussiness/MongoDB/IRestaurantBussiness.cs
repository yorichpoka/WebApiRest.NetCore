using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiRest.NetCore.Domain.Models.MongoDB;

namespace WebApiRest.NetCore.Domain.Interfaces.Bussiness.MongoDB
{
    public interface IRestaurantBussiness
    {
        Task<RestaurantModel> Create(RestaurantModel obj);

        Task<RestaurantModel> Read(string id);

        Task<IEnumerable<RestaurantModel>> Read(int top);

        Task Update(RestaurantModel obj);

        Task Delete(string restaurant_id);

        Task Delete(string[] restaurant_ids);
    }
}