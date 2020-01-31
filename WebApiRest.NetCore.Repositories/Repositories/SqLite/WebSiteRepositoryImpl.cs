using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using WebApiRest.NetCore.Domain.Interfaces.Repositories.SqLite;
using WebApiRest.NetCore.Domain.Models.SqLite;
using WebApiRest.NetCore.Repositories.Contexts;
using WebApiRest.NetCore.Repositories.Entities.SqLite;

namespace WebApiRest.NetCore.Repositories.Repositories.SqLite
{
    public class WebSiteRepositoryImpl : IWebSiteRepository
    {
        private readonly DataBaseSQLiteContext _DataBaseSQLiteContext;
        private readonly IMapper _Mapper;

        public WebSiteRepositoryImpl(DataBaseSQLiteContext dataBaseSqLiteContext, IMapper mapper)
        {
            this._DataBaseSQLiteContext = dataBaseSqLiteContext;
            this._Mapper = mapper;
        }

        public Task<WebSiteModel> Create(WebSiteModel obj)
        {
            return
                Task.Factory.StartNew<WebSiteModel>(() =>
                {
                    var value = this._Mapper.Map<WebSiteModel, WebSite>(obj);

                    value.Created = DateTime.Now.ToString("s");

                    this._DataBaseSQLiteContext.WebSites.Add(value);

                    this._DataBaseSQLiteContext.SaveChanges();

                    return this._Mapper.Map<WebSite, WebSiteModel>(value);
                });
        }

        public Task Delete(int id)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    this._DataBaseSQLiteContext.WebSites.Remove(
                        this._DataBaseSQLiteContext.WebSites.Find(id)
                    );

                    this._DataBaseSQLiteContext.SaveChanges();
                });
        }

        public Task Delete(int[] ids)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    this._DataBaseSQLiteContext.WebSites.RemoveRange(
                        this._DataBaseSQLiteContext.WebSites.Where(l => ids.Contains(l.Key))
                    );

                    this._DataBaseSQLiteContext.SaveChanges();
                });
        }

        public Task<WebSiteModel> Read(int id)
        {
            return
                Task.Factory.StartNew<WebSiteModel>(() =>
                {
                    var value = this._DataBaseSQLiteContext.WebSites.FirstOrDefault(l => l.Key == id);

                    return this._Mapper.Map<WebSite, WebSiteModel>(value);
                });
        }

        public Task<IEnumerable<WebSiteModel>> Read()
        {
            return
                Task.Factory.StartNew<IEnumerable<WebSiteModel>>(() =>
                {
                    var value = this._DataBaseSQLiteContext.WebSites.ToList();

                    return this._Mapper.Map<IEnumerable<WebSite>, IEnumerable<WebSiteModel>>(value);
                });
        }

        public Task Update(WebSiteModel obj)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    var value = this._DataBaseSQLiteContext.WebSites.Find(obj.Key);

                    value.ExtUpdate(obj);

                    this._DataBaseSQLiteContext.WebSites.Update(value);

                    this._DataBaseSQLiteContext.SaveChanges();
                });
        }
    }
}