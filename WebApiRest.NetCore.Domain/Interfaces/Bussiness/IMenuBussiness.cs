﻿using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiRest.NetCore.Domain.Models;

namespace WebApiRest.NetCore.Domain.Interfaces.Bussiness
{
    public interface IMenuBussiness
    {
        Task<MenuModel> Create(MenuModel obj);

        Task<MenuModel> Read(int id);

        Task<IEnumerable<MenuModel>> Read();

        Task Update(MenuModel obj);

        Task Delete(int id);
    }
}