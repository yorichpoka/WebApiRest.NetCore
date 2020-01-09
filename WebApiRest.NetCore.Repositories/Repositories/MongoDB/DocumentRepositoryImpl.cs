using AutoMapper;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using System;
using System.IO;
using System.Threading.Tasks;
using WebApiRest.NetCore.Domain.Interfaces.Repositories;
using WebApiRest.NetCore.Domain.Models.MongoDB;
using WebApiRest.NetCore.Repositories.Entities.MongoDB;

namespace WebApiRest.NetCore.Repositories.Repositories.MongoDB
{
    public class DocumentRepositoryImpl : IDocumentRepository
    {
        private readonly IMapper _Mapper;
        private readonly IMongoCollection<Document> _Collection;
        private readonly MongoGridFS _GridFileSystem;
        private readonly string _CollectionName = "documents";

        [Obsolete]
        public DocumentRepositoryImpl(IMongoDatabase mongoDatabase, IMapper mapper)
        {
            this._Mapper = mapper;
            this._Collection = mongoDatabase.GetCollection<Document>(this._CollectionName);
            this._GridFileSystem = new MongoGridFS(mongoDatabase as MongoDatabase);
        }

        public Task<DocumentModel> Create(DocumentModel obj)
        {
            return
                Task.Factory.StartNew<DocumentModel>(() =>
                {
                    var entity = this._Mapper.Map<DocumentModel, Document>(obj);

                    // Get file info
                    var gridFileInfo = this._GridFileSystem.Upload(
                                            new MemoryStream(obj.Content),
                                            obj.Name
                                        );

                    // Set id value
                    entity.Document_Id = DateTime.Now.Ticks.ToString();

                    // Set id of document
                    entity.Content_Id = gridFileInfo.Id.AsObjectId;

                    this._Collection.InsertOne(entity);

                    return this._Mapper.Map<Document, DocumentModel>(entity);
                });
        }

        public Task Delete(string document_id)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    var deletedResult = this._Collection.DeleteOne(l => l.Document_Id == document_id);
                });
        }

        public Task<DocumentModel> Read(string document_id)
        {
            return
                Task.Factory.StartNew<DocumentModel>(() =>
                {
                    var entity = this._Collection.Find(l => l.Document_Id == document_id).FirstOrDefault();

                    // Get file info
                    var gridFileInfo = (this._Collection as MongoDatabase).GridFS.FindOne(Query.EQ("_id", entity.Content_Id));

                    // Download file
                    if (!gridFileInfo.Exists)
                        throw new FileNotFoundException("The content byte of file was not found!");

                    // Get document model of file
                    var document = this._Mapper.Map<Document, DocumentModel>(entity);

                    // Get content byte of document
                    using (var stream = gridFileInfo.OpenRead())
                    {
                        var memoryStream = new MemoryStream();
                        stream.CopyTo(memoryStream);

                        document.Content = memoryStream.ToArray();
                    }

                    return document;
                });
        }

        public Task Update(DocumentModel obj)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    var entity = this._Collection.Find(l => l.Document_Id == obj.Document_Id).FirstOrDefault();

                    // Get file info
                    var gridFileInfo = (this._Collection as MongoDatabase).GridFS.FindOne(Query.EQ("_id", entity.Content_Id));

                    // Update content
                    using (var stream = gridFileInfo.OpenWrite())
                    {
                        stream.Write(obj.Content, 0, obj.Content.Length);
                    }

                    entity.ExtUpdate(obj);

                    var updateDefinition = Builders<Document>.Update.Set(l => l.Date, entity.Date)
                                                                    .Set(l => l.Name, entity.Name)
                                                                    .Set(l => l.Content_Id, entity.Content_Id)
                                                                    .Set(l => l.Extension, entity.Extension)
                                                                    .Set(l => l.FullName, entity.FullName)
                                                                    .Set(l => l.Size, entity.Size);

                    var updateResult = this._Collection.UpdateOne<Document>(l => l.Document_Id == obj.Document_Id, updateDefinition);
                });
        }
    }
}