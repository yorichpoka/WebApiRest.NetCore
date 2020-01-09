using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiRest.NetCore.Domain.Interfaces.Bussiness.SqLite;
using WebApiRest.NetCore.Domain.Interfaces.Repositories.SqLite;
using WebApiRest.NetCore.Domain.Models.SqLite;

namespace WebApiRest.NetCore.Bussiness.SqLite
{
    public class WebSiteBussinessImpl : IWebSiteBussiness
    {
        private readonly IWebSiteRepository _WebSiteRepository;

        public WebSiteBussinessImpl(IWebSiteRepository repository)
        {
            this._WebSiteRepository = repository;
        }

        public Task<WebSiteModel> Create(WebSiteModel obj)
        {
            return
                this._WebSiteRepository.Create(obj);
        }

        public Task Delete(int id)
        {
            return
                this._WebSiteRepository.Delete(id);
        }

        public Task Delete(int[] ids)
        {
            return
                this._WebSiteRepository.Delete(ids);
        }

        public Task<WebSiteModel> Read(int id)
        {
            return
                this._WebSiteRepository.Read(id);
        }

        public Task<IEnumerable<WebSiteModel>> Read()
        {
            return
                this._WebSiteRepository.Read();
        }

        public Task Update(WebSiteModel obj)
        {
            return
                this._WebSiteRepository.Update(obj);
        }
    }
}