using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiRest.NetCore.Domain.Interfaces;
using WebApiRest.NetCore.Domain.Models;
using WebApiRest.NetCore.Repositories.Contexts;
using WebApiRest.NetCore.Repositories.Entities.SQLServer;
using WebApiRest.NetCore.Tools;

namespace WebApirest.NetCore.Bussiness.SQLServer
{
    public class AuthorizationDaoImpl : IAuthorizationDao
    {
        private readonly DataBaseSQLServerContext _DataBaseSQLServerContext;

        public AuthorizationDaoImpl(DataBaseSQLServerContext dataBaseSQLServerContext)
        {
            this._DataBaseSQLServerContext = dataBaseSQLServerContext;
        }

        public Task<AuthorizationModel> Create(AuthorizationModel obj)
        {
            return
                Task.Factory.StartNew<AuthorizationModel>(() =>
                {
                    var value = MapperSingleton.Instance.Map<AuthorizationModel, Authorization>(obj);

                    this._DataBaseSQLServerContext.Authorizations.Add(value);

                    this._DataBaseSQLServerContext.SaveChanges();

                    return MapperSingleton.Instance.Map<Authorization, AuthorizationModel>(value);
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

        public Task<AuthorizationModel> Read(int idRole, int idMenu)
        {
            return
                Task.Factory.StartNew<AuthorizationModel>(() =>
                {
                    var value = this._DataBaseSQLServerContext.Authorizations.FirstOrDefaultAsync(l => l.IdRole == idRole && l.IdMenu == idMenu);

                    return MapperSingleton.Instance.Map<Authorization, AuthorizationModel>(value.Result);
                });
        }

        public Task<IEnumerable<AuthorizationModel>> Read()
        {
            return
                Task.Factory.StartNew<IEnumerable<AuthorizationModel>>(() =>
                {
                    var value = this._DataBaseSQLServerContext.Authorizations.ToList();

                    return MapperSingleton.Instance.Map<IEnumerable<Authorization>, IEnumerable<AuthorizationModel>>(value);
                });
        }

        public Task Update(AuthorizationModel obj)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    var value = this._DataBaseSQLServerContext.Authorizations.FirstOrDefaultAsync(l => l.IdRole == obj.IdRole && l.IdMenu == obj.IdMenu);

                    //value.Result.ExtUpdate(obj);

                    this._DataBaseSQLServerContext.Authorizations.Update(value.Result);

                    this._DataBaseSQLServerContext.SaveChanges();
                });
        }
    }
}