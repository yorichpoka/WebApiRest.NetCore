using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiRest.NetCore.Models.Dao;
using WebApiRest.NetCore.Models.Dtos;
using WebApiRest.NetCore.Models.Entity;
using WebApiRest.NetCore.Models.Entity.SQLServer;
using WebApiRest.NetCore.Models.Tools;

namespace WebApiRest.NetCore.Models.DaoImpl.SQLServer
{
    public class AuthorizationDaoImpl : IAuthorizationDao
    {
        private readonly DataBaseSQLServerContext _DataBaseSQLServerContext;

        public AuthorizationDaoImpl(DataBaseSQLServerContext dataBaseSQLServerContext)
        {
            this._DataBaseSQLServerContext = dataBaseSQLServerContext;
        }

        public Task<AuthorizationDto> Create(AuthorizationDto obj)
        {
            return
                Task.Factory.StartNew<AuthorizationDto>(() =>
                {
                    var value = MapperSingleton.Instance.Map<AuthorizationDto, Authorization>(obj);

                    this._DataBaseSQLServerContext.Authorizations.Add(value);

                    this._DataBaseSQLServerContext.SaveChanges();

                    return MapperSingleton.Instance.Map<Authorization, AuthorizationDto>(value);
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

        public Task<AuthorizationDto> Read(int idRole, int idMenu)
        {
            return
                Task.Factory.StartNew<AuthorizationDto>(() =>
                {
                    var value = this._DataBaseSQLServerContext.Authorizations.FirstOrDefaultAsync(l => l.IdRole == idRole && l.IdMenu == idMenu);

                    return MapperSingleton.Instance.Map<Authorization, AuthorizationDto>(value.Result);
                });
        }

        public Task<IEnumerable<AuthorizationDto>> Read()
        {
            return
                Task.Factory.StartNew<IEnumerable<AuthorizationDto>>(() =>
                {
                    var value = this._DataBaseSQLServerContext.Authorizations.ToList();

                    return MapperSingleton.Instance.Map<IEnumerable<Authorization>, IEnumerable<AuthorizationDto>>(value);
                });
        }

        public Task Update(AuthorizationDto obj)
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