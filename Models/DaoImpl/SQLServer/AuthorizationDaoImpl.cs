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
        public Task<AuthorizationDto> Create(AuthorizationDto obj)
        {
            return
                Task.Factory.StartNew<AuthorizationDto>(() =>
                {
                    using (var db = new TestDBEntities())
                    {
                        var value = MapperSingleton.Instance.Map<AuthorizationDto, Authorization>(obj);

                        db.Authorizations.Add(value);

                        db.SaveChanges();

                        return MapperSingleton.Instance.Map<Authorization, AuthorizationDto>(value);
                    }
                });
        }

        public Task Delete(int idRole, int idMenu)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    using (var db = new TestDBEntities())
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
                    using (var db = new TestDBEntities())
                    {
                        var value = db.Authorizations.FirstOrDefaultAsync(l => l.IdRole == idRole && l.IdMenu == idMenu);

                        return MapperSingleton.Instance.Map<Authorization, AuthorizationDto>(value.Result);
                    }
                });
        }

        public Task<IEnumerable<AuthorizationDto>> Read()
        {
            return
                Task.Factory.StartNew<IEnumerable<AuthorizationDto>>(() =>
                {
                    using (var db = new TestDBEntities())
                    {
                        var value = db.Authorizations.ToList();

                        return MapperSingleton.Instance.Map<IEnumerable<Authorization>, IEnumerable<AuthorizationDto>>(value);
                    }
                });
        }

        public Task Update(AuthorizationDto obj)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    using (var db = new TestDBEntities())
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