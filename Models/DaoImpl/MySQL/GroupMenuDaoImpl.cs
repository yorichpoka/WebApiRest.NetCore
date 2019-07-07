using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiRest.NetCore.Models.Dao;
using WebApiRest.NetCore.Models.Dtos;
using WebApiRest.NetCore.Models.Entity.MySQL;
using WebApiRest.NetCore.Models.Tools;

namespace WebApiRest.NetCore.Models.DaoImpl.MySQL
{
    public class GroupMenuDaoImpl : IGroupMenuDao
    {
        private readonly DataBaseMySQLContext _DataBaseMySQLContext;

        public GroupMenuDaoImpl(DataBaseMySQLContext dataBaseMySQLContext)
        {
            this._DataBaseMySQLContext = dataBaseMySQLContext;
        }

        public Task<GroupMenuDto> Create(GroupMenuDto obj)
        {
            return
                Task.Factory.StartNew<GroupMenuDto>(() =>
                {
                    var value = MapperSingleton.Instance.Map<GroupMenuDto, TblGroupMenu>(obj);

                    this._DataBaseMySQLContext.GroupMenus.Add(value);

                    this._DataBaseMySQLContext.SaveChanges();

                    return MapperSingleton.Instance.Map<TblGroupMenu, GroupMenuDto>(value);
                });
        }

        public Task Delete(int id)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    this._DataBaseMySQLContext.GroupMenus.Remove(
                        this._DataBaseMySQLContext.GroupMenus.Find(id)
                    );

                    this._DataBaseMySQLContext.SaveChanges();
                });
        }

        public Task<GroupMenuDto> Read(int id)
        {
            return
                Task.Factory.StartNew<GroupMenuDto>(() =>
                {
                    var value = this._DataBaseMySQLContext.GroupMenus.FindAsync(id);

                    return MapperSingleton.Instance.Map<TblGroupMenu, GroupMenuDto>(value.Result);
                });
        }

        public Task<IEnumerable<GroupMenuDto>> Read()
        {
            return
                Task.Factory.StartNew<IEnumerable<GroupMenuDto>>(() =>
                {
                    var value = this._DataBaseMySQLContext.GroupMenus.ToList();

                    return MapperSingleton.Instance.Map<IEnumerable<TblGroupMenu>, IEnumerable<GroupMenuDto>>(value);
                });
        }

        public Task Update(GroupMenuDto obj)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    var value = this._DataBaseMySQLContext.GroupMenus.Find(obj.Id);

                    value.ExtUpdate(obj);

                    this._DataBaseMySQLContext.GroupMenus.Update(value);

                    this._DataBaseMySQLContext.SaveChanges();
                });
        }
    }
}