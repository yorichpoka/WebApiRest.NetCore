using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiRest.NetCore.Domain.Interfaces.Bussiness.MongoDB;
using WebApiRest.NetCore.Domain.Interfaces.Repositories.MongoDB;
using WebApiRest.NetCore.Domain.Models.MongoDB;

namespace WebApiRest.NetCore.Bussiness.MySql
{
    public class RestaurantBussinessImpl : IRestaurantBussiness
    {
        private readonly IRestaurantRepository _RestaurantRepository;

        public RestaurantBussinessImpl(IRestaurantRepository repository)
        {
            this._RestaurantRepository = repository;
        }

        public Task<RestaurantModel> Create(RestaurantModel obj)
        {
            return
                this._RestaurantRepository.Create(obj);
        }

        public Task Delete(string restaurant_id)
        {
            return
                this._RestaurantRepository.Delete(restaurant_id);
        }

        public Task Delete(string[] restaurant_ids)
        {
            return
                this._RestaurantRepository.Delete(restaurant_ids);
        }

        public Task<RestaurantModel> Read(string id)
        {
            return
                this._RestaurantRepository.Read(id);
        }

        public Task<IEnumerable<RestaurantModel>> Read(int top)
        {
            return
                this._RestaurantRepository.Read(top);
        }

        public Task Update(RestaurantModel obj)
        {
            return
                this._RestaurantRepository.Update(obj);
        }
    }
}