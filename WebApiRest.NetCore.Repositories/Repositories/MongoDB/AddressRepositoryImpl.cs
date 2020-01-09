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
    public class AddressRepositoryImpl : IAddressRepository
    {
        private readonly IMapper _Mapper;
        private readonly IMongoCollection<Address> _Collection;
        private readonly string _CollectionName = "address";

        public AddressRepositoryImpl(IMongoDatabase mongoDatabase, IMapper mapper)
        {
            this._Mapper = mapper;
            this._Collection = mongoDatabase.GetCollection<Address>(this._CollectionName);
        }

        public Task<AddressModel> Create(AddressModel obj)
        {
            return
                Task.Factory.StartNew<AddressModel>(() =>
                {
                    var entity = this._Mapper.Map<AddressModel, Address>(obj);

                    // Set id value
                    entity.Address_Id = DateTime.Now.Ticks.ToString();

                    this._Collection.InsertOne(entity);

                    return this._Mapper.Map<Address, AddressModel>(entity);
                });
        }

        public Task Delete(string address_id)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    var deletedResult = this._Collection.DeleteOne(l => l.Address_Id == address_id);
                });
        }

        public Task Delete(string[] address_ids)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    var deletedResult = this._Collection.DeleteMany(l => address_ids.Contains(l.Address_Id));
                });
        }

        public Task<AddressModel> Read(string address_id)
        {
            return
                Task.Factory.StartNew<AddressModel>(() =>
                {
                    var entity = this._Collection.Find(l => l.Address_Id == address_id).FirstOrDefault();

                    return this._Mapper.Map<Address, AddressModel>(entity);
                });
        }

        public Task<IEnumerable<AddressModel>> Read(int top)
        {
            return
                Task.Factory.StartNew<IEnumerable<AddressModel>>(() =>
                {
                    var entities = this._Collection.Find(l => l.Address_Id != null).Limit(top).ToList();

                    return this._Mapper.Map<IEnumerable<Address>, IEnumerable<AddressModel>>(entities);
                });
        }

        public Task<IEnumerable<AddressModel>> Read()
        {
            return
                Task.Factory.StartNew<IEnumerable<AddressModel>>(() =>
                {
                    var entities = this._Collection.Find(l => l.Address_Id != null).ToList();

                    return this._Mapper.Map<IEnumerable<Address>, IEnumerable<AddressModel>>(entities);
                });
        }

        public Task Update(AddressModel obj)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    var entity = this._Collection.Find(l => l.Address_Id == obj.Address_Id).FirstOrDefault();

                    entity.ExtUpdate(obj);

                    var updateDefinition = Builders<Address>.Update.Set(l => l.Building, entity.Building)
                                                                   .Set(l => l.Street, entity.Street)
                                                                   .Set(l => l.ZipCode, entity.ZipCode)
                                                                   .Set(l => l.Coordinate, entity.Coordinate);

                    var updateResult = this._Collection.UpdateOne<Address>(l => l.Address_Id == obj.Address_Id, updateDefinition);
                });
        }
    }
}