﻿using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiRest.NetCore.Domain.Models;

namespace WebApiRest.NetCore.Domain.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        Task<RoleModel> Create(RoleModel obj);

        Task<RoleModel> Read(int id);

        Task<IEnumerable<RoleModel>> Read();

        Task Update(RoleModel obj);

        Task Delete(int id);
    }
}