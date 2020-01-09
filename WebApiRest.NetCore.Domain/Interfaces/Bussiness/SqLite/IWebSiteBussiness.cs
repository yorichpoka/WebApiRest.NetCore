﻿using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiRest.NetCore.Domain.Models.SqLite;

namespace WebApiRest.NetCore.Domain.Interfaces.Bussiness.SqLite
{
    public interface IWebSiteBussiness
    {
        Task<WebSiteModel> Create(WebSiteModel obj);

        Task<WebSiteModel> Read(int id);

        Task<IEnumerable<WebSiteModel>> Read();

        Task Update(WebSiteModel obj);

        Task Delete(int id);

        Task Delete(int[] ids);
    }
}