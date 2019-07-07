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
    public class GroupMenuDaoImpl : IGroupMenuDao
    {
        private readonly DataBaseSQLServerContext _DataBaseSQLServerContext;

        public GroupMenuDaoImpl(DataBaseSQLServerContext dataBaseSQLServerContext)
        {
            this._DataBaseSQLServerContext = dataBaseSQLServerContext;
        }

        public Task<GroupMenuDto> Create(GroupMenuDto obj)
        {
            return
                Task.Factory.StartNew<GroupMenuDto>(() =>
                {
                    var value = MapperSingleton.Instance.Map<GroupMenuDto, GroupMenu>(obj);

                    this._DataBaseSQLServerContext.GroupMenus.Add(value);

                    this._DataBaseSQLServerContext.SaveChanges();

                    return MapperSingleton.Instance.Map<GroupMenu, GroupMenuDto>(value);
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

        public Task<GroupMenuDto> Read(int id)
        {
            return
                Task.Factory.StartNew<GroupMenuDto>(() =>
                {
                    var value = this._DataBaseSQLServerContext.GroupMenus.FindAsync(id);

                    return MapperSingleton.Instance.Map<GroupMenu, GroupMenuDto>(value.Result);
                });
        }

        public Task<IEnumerable<GroupMenuDto>> Read()
        {
            return
                Task.Factory.StartNew<IEnumerable<GroupMenuDto>>(() =>
                {
                    var value = this._DataBaseSQLServerContext.GroupMenus.ToList();

                    return MapperSingleton.Instance.Map<IEnumerable<GroupMenu>, IEnumerable<GroupMenuDto>>(value);
                });
        }

        public Task Update(GroupMenuDto obj)
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