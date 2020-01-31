using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiRest.NetCore.Domain.Interfaces.Bussiness;
using WebApiRest.NetCore.Domain.Interfaces.Repositories;
using WebApiRest.NetCore.Domain.Models;

namespace WebApiRest.NetCore.Bussiness.MySql
{
    public class UserBussinessImpl : UserHubBussiness, IUserBussiness
    {
        private readonly IUserRepository _UserRepository;

        public UserBussinessImpl(IUserRepository repository)
        {
            this._UserRepository = repository;
        }

        public Task<UserModel> Create(UserModel obj)
        {
            return
                this._UserRepository.Create(obj);
        }

        public Task Delete(int id)
        {
            return
                this._UserRepository.Delete(id);
        }

        public Task Delete(int[] ids)
        {
            return
                this._UserRepository.Delete(ids);
        }

        public Task<UserModel> Read(int id)
        {
            return
                this._UserRepository.Read(id);
        }

        public Task<UserModel> Read(string login, string password)
        {
            return
                this._UserRepository.Read(login, password);
        }

        public Task<IEnumerable<UserModel>> Read()
        {
            return
                this._UserRepository.Read();
        }

        public Task<IEnumerable<UserRoleModel>> ReadWithRoles()
        {
            return
                this._UserRepository.ReadWithRoles();
        }

        public Task<UserModel> Update(UserModel obj)
        {
            return
                this._UserRepository.Update(obj);
        }
    }
}