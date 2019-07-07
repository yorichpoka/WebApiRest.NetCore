using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiRest.NetCore.Models.Dao;
using WebApiRest.NetCore.Models.Dtos;
using WebApiRest.NetCore.Models.Entity.MySQL;
using WebApiRest.NetCore.Models.Tools;

namespace WebApiRest.NetCore.Models.DaoImpl.MySQL
{
    public class AuthorizationDaoImpl : IAuthorizationDao
    {
        private readonly DataBaseMySQLContext _DataBaseMySQLContext;

        public AuthorizationDaoImpl(DataBaseMySQLContext _dataBaseMySQLContext)
        {
            this._DataBaseMySQLContext = _dataBaseMySQLContext;
        }

        public Task<AuthorizationDto> Create(AuthorizationDto obj)
        {
            return
                Task.Factory.StartNew<AuthorizationDto>(() =>
                {
                    var value = MapperSingleton.Instance.Map<AuthorizationDto, TblAuthorization>(obj);

                    this._DataBaseMySQLContext.Authorizations.Add(value);

                    this._DataBaseMySQLContext.SaveChanges();

                    return MapperSingleton.Instance.Map<TblAuthorization, AuthorizationDto>(value);
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

        public Task<AuthorizationDto> Read(int idRole, int idMenu)
        {
            return
                Task.Factory.StartNew<AuthorizationDto>(() =>
                {
                    var value = this._DataBaseMySQLContext.Authorizations.FirstOrDefaultAsync(l => l.IdRole == idRole && l.IdMenu == idMenu);

                    return MapperSingleton.Instance.Map<TblAuthorization, AuthorizationDto>(value.Result);
                });
        }

        public Task<IEnumerable<AuthorizationDto>> Read()
        {
            return
                Task.Factory.StartNew<IEnumerable<AuthorizationDto>>(() =>
                {
                    var value = this._DataBaseMySQLContext.Authorizations.ToList();

                    return MapperSingleton.Instance.Map<IEnumerable<TblAuthorization>, IEnumerable<AuthorizationDto>>(value);
                });
        }

        public Task Update(AuthorizationDto obj)
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