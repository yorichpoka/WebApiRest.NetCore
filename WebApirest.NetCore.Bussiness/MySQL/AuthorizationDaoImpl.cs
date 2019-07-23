using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiRest.NetCore.Domain.Interfaces;
using WebApiRest.NetCore.Domain.Models;
using WebApiRest.NetCore.Repositories.Contexts;
using WebApiRest.NetCore.Repositories.Entities.MySQL;
using WebApiRest.NetCore.Tools;

namespace WebApirest.NetCore.Bussiness.MySQL
{
    public class AuthorizationDaoImpl : IAuthorizationDao
    {
        private readonly DataBaseMySQLContext _DataBaseMySQLContext;

        public AuthorizationDaoImpl(DataBaseMySQLContext _dataBaseMySQLContext)
        {
            this._DataBaseMySQLContext = _dataBaseMySQLContext;
        }

        public Task<AuthorizationModel> Create(AuthorizationModel obj)
        {
            return
                Task.Factory.StartNew<AuthorizationModel>(() =>
                {
                    var value = MapperSingleton.Instance.Map<AuthorizationModel, TblAuthorization>(obj);

                    this._DataBaseMySQLContext.Authorizations.Add(value);

                    this._DataBaseMySQLContext.SaveChanges();

                    return MapperSingleton.Instance.Map<TblAuthorization, AuthorizationModel>(value);
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

                    return MapperSingleton.Instance.Map<TblAuthorization, AuthorizationModel>(value.Result);
                });
        }

        public Task<IEnumerable<AuthorizationModel>> Read()
        {
            return
                Task.Factory.StartNew<IEnumerable<AuthorizationModel>>(() =>
                {
                    var value = this._DataBaseMySQLContext.Authorizations.ToList();

                    return MapperSingleton.Instance.Map<IEnumerable<TblAuthorization>, IEnumerable<AuthorizationModel>>(value);
                });
        }

        public Task Update(AuthorizationModel obj)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    var value = this._DataBaseMySQLContext.Authorizations.FirstOrDefaultAsync(l => l.IdRole == obj.IdRole && l.IdMenu == obj.IdMenu);

                    //value.Result.ExtUpdate(obj);

                    this._DataBaseMySQLContext.Authorizations.Update(value.Result);

                    this._DataBaseMySQLContext.SaveChanges();
                });
        }
    }
}