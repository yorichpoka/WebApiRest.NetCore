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
        public Task<AuthorizationDto> Create(AuthorizationDto obj)
        {
            return
                Task.Factory.StartNew<AuthorizationDto>(() =>
                {
                    using (var db = new TestDBMySQLEntities())
                    {
                        var value = MapperSingleton.Instance.Map<AuthorizationDto, TblAuthorization>(obj);

                        db.Authorizations.Add(value);

                        db.SaveChanges();

                        return MapperSingleton.Instance.Map<TblAuthorization, AuthorizationDto>(value);
                    }
                });
        }

        public Task Delete(int idRole, int idMenu)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    using (var db = new TestDBMySQLEntities())
                    {
                        db.Authorizations.Remove(
                            db.Authorizations.FirstOrDefault(l => l.IdRole == idRole && l.IdMenu == idMenu)
                        );

                        db.SaveChanges();
                    }
                });
        }

        public Task<AuthorizationDto> Read(int idRole, int idMenu)
        {
            return
                Task.Factory.StartNew<AuthorizationDto>(() =>
                {
                    using (var db = new TestDBMySQLEntities())
                    {
                        var value = db.Authorizations.FirstOrDefaultAsync(l => l.IdRole == idRole && l.IdMenu == idMenu);

                        return MapperSingleton.Instance.Map<TblAuthorization, AuthorizationDto>(value.Result);
                    }
                });
        }

        public Task<IEnumerable<AuthorizationDto>> Read()
        {
            return
                Task.Factory.StartNew<IEnumerable<AuthorizationDto>>(() =>
                {
                    using (var db = new TestDBMySQLEntities())
                    {
                        var value = db.Authorizations.ToList();

                        return MapperSingleton.Instance.Map<IEnumerable<TblAuthorization>, IEnumerable<AuthorizationDto>>(value);
                    }
                });
        }

        public Task Update(AuthorizationDto obj)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    using (var db = new TestDBMySQLEntities())
                    {
                        var value = db.Authorizations.FirstOrDefaultAsync(l => l.IdRole == obj.IdRole && l.IdMenu == obj.IdMenu);

                        //value.Result.ExtUpdate(obj);

                        db.Authorizations.Update(value.Result);

                        db.SaveChanges();
                    }
                });
        }
    }
}