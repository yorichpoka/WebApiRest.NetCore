using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiRest.NetCore.Domain.Interfaces.Repositories;
using WebApiRest.NetCore.Domain.Models;
using WebApiRest.NetCore.Repositories.Contexts;
using SqlServerPkg = WebApiRest.NetCore.Repositories.Entities.SqlServer;

namespace WebApiRest.NetCore.Repositories.Repositories.SqlServer
{
    public class AuthorizationRepositoryImpl : IAuthorizationRepository
    {
        private readonly DataBaseSQLServerContext _DataBaseSQLServerContext;
        private readonly IMapper _Mapper;

        public AuthorizationRepositoryImpl(DataBaseSQLServerContext dataBaseSQLServerContext, IMapper mapper)
        {
            this._DataBaseSQLServerContext = dataBaseSQLServerContext;
            this._Mapper = mapper;
        }

        public Task<AuthorizationModel> Create(AuthorizationModel obj)
        {
            return
                Task.Factory.StartNew<AuthorizationModel>(() =>
                {
                    var value = this._Mapper.Map<AuthorizationModel, SqlServerPkg.Authorization>(obj);

                    this._DataBaseSQLServerContext.Authorizations.Add(value);

                    this._DataBaseSQLServerContext.SaveChanges();

                    return this._Mapper.Map<SqlServerPkg.Authorization, AuthorizationModel>(value);
                });
        }

        public Task Delete(int idRole, int idMenu)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    this._DataBaseSQLServerContext.Authorizations.Remove(
                        this._DataBaseSQLServerContext.Authorizations.FirstOrDefault(l => l.IdRole == idRole && l.IdMenu == idMenu)
                    );

                    this._DataBaseSQLServerContext.SaveChanges();
                });
        }

        public Task Delete(int[][] ids)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    this._DataBaseSQLServerContext.Authorizations.RemoveRange(
                        this._DataBaseSQLServerContext.Authorizations.Where(l => ids.Count(ll => ll[0] == l.IdRole && ll[1] == l.IdMenu) != 0)
                    );

                    this._DataBaseSQLServerContext.SaveChanges();
                });
        }

        public Task<AuthorizationModel> Read(int idRole, int idMenu)
        {
            return
                Task.Factory.StartNew<AuthorizationModel>(() =>
                {
                    var value = this._DataBaseSQLServerContext.Authorizations.FirstOrDefaultAsync(l => l.IdRole == idRole && l.IdMenu == idMenu);

                    return this._Mapper.Map<SqlServerPkg.Authorization, AuthorizationModel>(value.Result);
                });
        }

        public Task<IEnumerable<AuthorizationModel>> Read()
        {
            return
                Task.Factory.StartNew<IEnumerable<AuthorizationModel>>(() =>
                {
                    var value = this._DataBaseSQLServerContext.Authorizations.ToList();

                    return this._Mapper.Map<IEnumerable<SqlServerPkg.Authorization>, IEnumerable<AuthorizationModel>>(value);
                });
        }

        public Task Update(AuthorizationModel obj)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    var value = this._DataBaseSQLServerContext.Authorizations.FirstOrDefaultAsync(l => l.IdRole == obj.IdRole && l.IdMenu == obj.IdMenu);

                    value.Result.ExtUpdate(obj);

                    this._DataBaseSQLServerContext.Authorizations.Update(value.Result);

                    this._DataBaseSQLServerContext.SaveChanges();
                });
        }
    }
}