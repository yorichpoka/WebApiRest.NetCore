﻿using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiRest.NetCore.Domain.Interfaces.Bussiness;
using WebApiRest.NetCore.Domain.Interfaces.Repositories;
using WebApiRest.NetCore.Domain.Models;

namespace WebApirest.NetCore.Bussiness.SQLServer
{
    public class AuthorizationBussinessImpl : IAuthorizationBussiness
    {
        private readonly IAuthorizationRepository _AuthorizationRepository;

        public AuthorizationBussinessImpl(IAuthorizationRepository repository)
        {
            this._AuthorizationRepository = repository;
        }

        public Task<AuthorizationModel> Create(AuthorizationModel obj)
        {
            return
                this._AuthorizationRepository.Create(obj);
        }

        public Task Delete(int idRole, int idMenu)
        {
            return
                this._AuthorizationRepository.Delete(idRole, idMenu);
        }

        public Task<AuthorizationModel> Read(int idRole, int idMenu)
        {
            return
                this._AuthorizationRepository.Read(idRole, idMenu);
        }

        public Task<IEnumerable<AuthorizationModel>> Read()
        {
            return
                this._AuthorizationRepository.Read();
        }

        public Task Update(AuthorizationModel obj)
        {
            return
                this._AuthorizationRepository.Update(obj);
        }
    }
}