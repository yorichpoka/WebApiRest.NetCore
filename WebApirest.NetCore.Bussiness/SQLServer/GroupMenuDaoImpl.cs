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
    public class GroupMenuDaoImpl : IGroupMenuDao
    {
        private readonly DataBaseSQLServerContext _DataBaseSQLServerContext;

        public GroupMenuDaoImpl(DataBaseSQLServerContext dataBaseSQLServerContext)
        {
            this._DataBaseSQLServerContext = dataBaseSQLServerContext;
        }

        public Task<GroupMenuModel> Create(GroupMenuModel obj)
        {
            return
                Task.Factory.StartNew<GroupMenuModel>(() =>
                {
                    var value = MapperSingleton.Instance.Map<GroupMenuModel, GroupMenu>(obj);

                    this._DataBaseSQLServerContext.GroupMenus.Add(value);

                    this._DataBaseSQLServerContext.SaveChanges();

                    return MapperSingleton.Instance.Map<GroupMenu, GroupMenuModel>(value);
                });
        }

        public Task Delete(int id)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    this._DataBaseSQLServerContext.GroupMenus.Remove(
                        this._DataBaseSQLServerContext.GroupMenus.Find(id)
                    );

                    this._DataBaseSQLServerContext.SaveChanges();
                });
        }

        public Task<GroupMenuModel> Read(int id)
        {
            return
                Task.Factory.StartNew<GroupMenuModel>(() =>
                {
                    var value = this._DataBaseSQLServerContext.GroupMenus.FindAsync(id);

                    return MapperSingleton.Instance.Map<GroupMenu, GroupMenuModel>(value.Result);
                });
        }

        public Task<IEnumerable<GroupMenuModel>> Read()
        {
            return
                Task.Factory.StartNew<IEnumerable<GroupMenuModel>>(() =>
                {
                    var value = this._DataBaseSQLServerContext.GroupMenus.ToList();

                    return MapperSingleton.Instance.Map<IEnumerable<GroupMenu>, IEnumerable<GroupMenuModel>>(value);
                });
        }

        public Task Update(GroupMenuModel obj)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    var value = this._DataBaseSQLServerContext.GroupMenus.Find(obj.Id);

                    value.ExtUpdate(obj);

                    this._DataBaseSQLServerContext.GroupMenus.Update(value);

                    this._DataBaseSQLServerContext.SaveChanges();
                });
        }
    }
}