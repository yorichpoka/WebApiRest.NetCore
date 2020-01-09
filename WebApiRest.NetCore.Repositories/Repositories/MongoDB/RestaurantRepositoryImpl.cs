using AutoMapper;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiRest.NetCore.Domain.Interfaces.Repositories.MongoDB;
using WebApiRest.NetCore.Domain.Models.MongoDB;
using WebApiRest.NetCore.Repositories.Entities.MongoDB;

namespace WebApiRest.NetCore.Repositories.Repositories.MongoDB
{
    public class RestaurantRepositoryImpl : IRestaurantRepository
    {
        private readonly IMapper _Mapper;
        private readonly IMongoCollection<Restaurant> _Collection;
        private readonly string _CollectionName = "restaurants";

        public RestaurantRepositoryImpl(IMongoDatabase mongoDatabase, IMapper mapper)
        {
            this._Mapper = mapper;
            this._Collection = mongoDatabase.GetCollection<Restaurant>(this._CollectionName);
        }

        public Task<RestaurantModel> Create(RestaurantModel obj)
        {
            return
                Task.Factory.StartNew<RestaurantModel>(() =>
                {
                    var entity = this._Mapper.Map<RestaurantModel, Restaurant>(obj);

                    // Set id value
                    entity.Restaurant_Id = DateTime.Now.Ticks.ToString();

                    this._Collection.InsertOne(entity);

                    return this._Mapper.Map<Restaurant, RestaurantModel>(entity);
                });
        }

        public Task Delete(string restaurant_id)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    var deletedResult = this._Collection.DeleteOne(l => l.Restaurant_Id == restaurant_id);
                });
        }

        public Task Delete(string[] restaurant_ids)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    var deletedResult = this._Collection.DeleteMany(l => restaurant_ids.Contains(l.Restaurant_Id));
                });
        }

        public Task<RestaurantModel> Read(string restaurant_id)
        {
            return
                Task.Factory.StartNew<RestaurantModel>(() =>
                {
                    var entity = this._Collection.Find(l => l.Restaurant_Id == restaurant_id).FirstOrDefault();

                    return this._Mapper.Map<Restaurant, RestaurantModel>(entity);
                });
        }

        public Task<IEnumerable<RestaurantModel>> Read(int top)
        {
            return
                Task.Factory.StartNew<IEnumerable<RestaurantModel>>(() =>
                {
                    var entities = this._Collection.Find(l => l.Restaurant_Id != null).Limit(top).ToList();

                    return this._Mapper.Map<IEnumerable<Restaurant>, IEnumerable<RestaurantModel>>(entities);
                });
        }

        public Task Update(RestaurantModel obj)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    var entity = this._Collection.Find(l => l.Restaurant_Id == obj.Restaurant_Id).FirstOrDefault();

                    entity.ExtUpdate(obj);

                    var updateDefinition = Builders<Restaurant>.Update.Set(l => l.Address, entity.Address)
                                                                      .Set(l => l.Borough, entity.Borough)
                                                                      .Set(l => l.Cuisine, entity.Cuisine)
                                                                      .Set(l => l.Grades, entity.Grades)
                                                                      .Set(l => l.Name, entity.Name);

                    var updateResult = this._Collection.UpdateOne<Restaurant>(l => l.Restaurant_Id == obj.Restaurant_Id, updateDefinition);
                });
        }
    }
}