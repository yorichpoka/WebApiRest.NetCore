using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiRest.NetCore.Domain.Interfaces.Repositories;
using WebApiRest.NetCore.Domain.Models;
using WebApiRest.NetCore.Repositories.Contexts;
using WebApiRest.NetCore.Repositories.Entities.MySQL;

namespace WebApiRest.NetCore.Repositories.MySQL
{
    public class AuthorizationRepositoryImpl : IAuthorizationRepository
    {
        private readonly DataBaseMySQLContext _DataBaseMySQLContext;
        private readonly IMapper _Mapper;

        public AuthorizationRepositoryImpl(DataBaseMySQLContext dataBaseMySQLContext, IMapper mapper)
        {
            this._DataBaseMySQLContext = dataBaseMySQLContext;
            this._Mapper = mapper;
        }

        public Task<AuthorizationModel> Create(AuthorizationModel obj)
        {
            return
                Task.Factory.StartNew<AuthorizationModel>(() =>
                {
                    var value = this._Mapper.Map<AuthorizationModel, TblAuthorization>(obj);

                    this._DataBaseMySQLContext.Authorizations.Add(value);

                    this._DataBaseMySQLContext.SaveChanges();

                    return this._Mapper.Map<TblAuthorization, AuthorizationModel>(value);
                });
        }

        public Task Delete(int idRole, int idMenu)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    this._DataBaseMySQLContext.Authorizations.Remove(
                        this._DataBaseMySQLContext.Authorizations.FirstOrDefault(l => l.IdRole == idRole && l.IdMenu == idMenu)
                    );

                    this._DataBaseMySQLContext.SaveChanges();
                });
        }

        public Task<AuthorizationModel> Read(int idRole, int idMenu)
        {
            return
                Task.Factory.StartNew<AuthorizationModel>(() =>
                {
                    var value = this._DataBaseMySQLContext.Authorizations.FirstOrDefaultAsync(l => l.IdRole == idRole && l.IdMenu == idMenu);

                    return this._Mapper.Map<TblAuthorization, AuthorizationModel>(value.Result);
                });
        }

        public Task<IEnumerable<AuthorizationModel>> Read()
        {
            return
                Task.Factory.StartNew<IEnumerable<AuthorizationModel>>(() =>
                {
                    var value = this._DataBaseMySQLContext.Authorizations.ToList();

                    return this._Mapper.Map<IEnumerable<TblAuthorization>, IEnumerable<AuthorizationModel>>(value);
                });
        }

        public Task Update(AuthorizationModel obj)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    var value = this._DataBaseMySQLContext.Authorizations.FirstOrDefaultAsync(l => l.IdRole == obj.IdRole && l.IdMenu == obj.IdMenu);

                    value.Result.ExtUpdate(obj);

                    this._DataBaseMySQLContext.Authorizations.Update(value.Result);

                    this._DataBaseMySQLContext.SaveChanges();
                });
        }
    }
}