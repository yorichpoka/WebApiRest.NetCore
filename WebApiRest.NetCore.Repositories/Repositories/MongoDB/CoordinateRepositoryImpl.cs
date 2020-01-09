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
    public class CoordinateRepositoryImpl : ICoordinateRepository
    {
        private readonly IMapper _Mapper;
        private readonly IMongoCollection<Coordinate> _Collection;
        private readonly string _CollectionName = "coordinates";

        public CoordinateRepositoryImpl(IMongoDatabase mongoDatabase, IMapper mapper)
        {
            this._Mapper = mapper;
            this._Collection = mongoDatabase.GetCollection<Coordinate>(this._CollectionName);
        }

        public Task<CoordinateModel> Create(CoordinateModel obj)
        {
            return
                Task.Factory.StartNew<CoordinateModel>(() =>
                {
                    var entity = this._Mapper.Map<CoordinateModel, Coordinate>(obj);

                    // Set id value
                    entity.Coordinate_Id = DateTime.Now.Ticks.ToString();

                    this._Collection.InsertOne(entity);

                    return this._Mapper.Map<Coordinate, CoordinateModel>(entity);
                });
        }

        public Task Delete(string coordinate_id)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    var deletedResult = this._Collection.DeleteOne(l => l.Coordinate_Id == coordinate_id);
                });
        }

        public Task Delete(string[] coordinate_ids)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    var deletedResult = this._Collection.DeleteMany(l => coordinate_ids.Contains(l.Coordinate_Id));
                });
        }

        public Task<CoordinateModel> Read(string coordinate_id)
        {
            return
                Task.Factory.StartNew<CoordinateModel>(() =>
                {
                    var entity = this._Collection.Find(l => l.Coordinate_Id == coordinate_id).FirstOrDefault();

                    return this._Mapper.Map<Coordinate, CoordinateModel>(entity);
                });
        }

        public Task<IEnumerable<CoordinateModel>> Read(int top)
        {
            return
                Task.Factory.StartNew<IEnumerable<CoordinateModel>>(() =>
                {
                    var entities = this._Collection.Find(l => l.Coordinate_Id != null).Limit(top).ToList();

                    return this._Mapper.Map<IEnumerable<Coordinate>, IEnumerable<CoordinateModel>>(entities);
                });
        }

        public Task<IEnumerable<CoordinateModel>> Read()
        {
            return
                Task.Factory.StartNew<IEnumerable<CoordinateModel>>(() =>
                {
                    var entities = this._Collection.Find(l => l.Coordinate_Id != null).ToList();

                    return this._Mapper.Map<IEnumerable<Coordinate>, IEnumerable<CoordinateModel>>(entities);
                });
        }

        public Task Update(CoordinateModel obj)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    var entity = this._Collection.Find(l => l.Coordinate_Id == obj.Coordinate_Id).FirstOrDefault();

                    entity.ExtUpdate(obj);

                    var updateDefinition = Builders<Coordinate>.Update.Set(l => l.Coordinates, entity.Coordinates)
                                                                      .Set(l => l.Type, entity.Type);

                    var updateResult = this._Collection.UpdateOne<Coordinate>(l => l.Coordinate_Id == obj.Coordinate_Id, updateDefinition);
                });
        }
    }
}