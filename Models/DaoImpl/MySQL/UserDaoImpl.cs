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
    public class UserDaoImpl : IUserDao
    {
        public Task<UserDto> Create(UserDto obj)
        {
            return
                Task.Factory.StartNew<UserDto>(() =>
                {
                    using (var db = new TestDBMySQLEntities())
                    {
                        var value = MapperSingleton.Instance.Map<UserDto, TblUser>(obj);

                        db.Users.Add(value);

                        db.SaveChanges();

                        return MapperSingleton.Instance.Map<TblUser, UserDto>(value);
                    }
                });
        }

        public Task Delete(int id)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    using (var db = new TestDBMySQLEntities())
                    {
                        db.Users.Remove(
                            db.Users.Find(id)
                        );

                        db.SaveChanges();
                    }
                });
        }

        public Task<UserDto> Read(int id)
        {
            return
                Task.Factory.StartNew<UserDto>(() =>
                {
                    using (var db = new TestDBMySQLEntities())
                    {
                        var value = db.Users.FindAsync(id);

                        return MapperSingleton.Instance.Map<TblUser, UserDto>(value.Result);
                    }
                });
        }

        public Task<UserDto> Read(string login, string password)
        {
            return
                Task.Factory.StartNew<UserDto>(() =>
                {
                    using (var db = new TestDBMySQLEntities())
                    {
                        var value = db.Users.FirstOrDefaultAsync(l => l.Login == login && l.Password == password);

                        return MapperSingleton.Instance.Map<TblUser, UserDto>(value.Result);
                    }
                });
        }

        public Task<IEnumerable<UserDto>> Read()
        {
            return
                Task.Factory.StartNew<IEnumerable<UserDto>>(() =>
                {
                    using (var db = new TestDBMySQLEntities())
                    {
                        var value = db.Users.ToList();

                        return MapperSingleton.Instance.Map<IEnumerable<TblUser>, IEnumerable<UserDto>>(value);
                    }
                });
        }

        public Task Update(UserDto obj)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    using (var db = new TestDBMySQLEntities())
                    {
                        var value = db.Users.Find(obj.Id);

                        value.ExtUpdate(obj);

                        db.Users.Update(value);

                        db.SaveChanges();
                    }
                });
        }
    }
}