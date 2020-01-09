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
    public class GradeRepositoryImpl : IGradeRepository
    {
        private readonly IMapper _Mapper;
        private readonly IMongoCollection<Grade> _Collection;
        private readonly string _CollectionName = "grades";

        public GradeRepositoryImpl(IMongoDatabase mongoDatabase, IMapper mapper)
        {
            this._Mapper = mapper;
            this._Collection = mongoDatabase.GetCollection<Grade>(this._CollectionName);
        }

        public Task<GradeModel> Create(GradeModel obj)
        {
            return
                Task.Factory.StartNew<GradeModel>(() =>
                {
                    var entity = this._Mapper.Map<GradeModel, Grade>(obj);

                    // Set id value
                    entity.Grade_Id = DateTime.Now.Ticks.ToString();

                    this._Collection.InsertOne(entity);

                    return this._Mapper.Map<Grade, GradeModel>(entity);
                });
        }

        public Task Delete(string grade_id)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    var deletedResult = this._Collection.DeleteOne(l => l.Grade_Id == grade_id);
                });
        }

        public Task Delete(string[] grade_ids)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    var deletedResult = this._Collection.DeleteMany(l => grade_ids.Contains(l.Grade_Id));
                });
        }

        public Task<GradeModel> Read(string grade_id)
        {
            return
                Task.Factory.StartNew<GradeModel>(() =>
                {
                    var entity = this._Collection.Find(l => l.Grade_Id == grade_id).FirstOrDefault();

                    return this._Mapper.Map<Grade, GradeModel>(entity);
                });
        }

        public Task<IEnumerable<GradeModel>> Read(int top)
        {
            return
                Task.Factory.StartNew<IEnumerable<GradeModel>>(() =>
                {
                    var entities = this._Collection.Find(l => l.Grade_Id != null).Limit(top).ToList();

                    return this._Mapper.Map<IEnumerable<Grade>, IEnumerable<GradeModel>>(entities);
                });
        }

        public Task<IEnumerable<GradeModel>> Read()
        {
            return
                Task.Factory.StartNew<IEnumerable<GradeModel>>(() =>
                {
                    var entities = this._Collection.Find(l => l.Grade_Id != null).ToList();

                    return this._Mapper.Map<IEnumerable<Grade>, IEnumerable<GradeModel>>(entities);
                });
        }

        public Task Update(GradeModel obj)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    var entity = this._Collection.Find(l => l.Grade_Id == obj.Grade_Id).FirstOrDefault();

                    entity.ExtUpdate(obj);

                    var updateDefinition = Builders<Grade>.Update.Set(l => l.Date, entity.Date)
                                                                 .Set(l => l.Name, entity.Name)
                                                                 .Set(l => l.Score, entity.Score);

                    var updateResult = this._Collection.UpdateOne<Grade>(l => l.Grade_Id == obj.Grade_Id, updateDefinition);
                });
        }
    }
}