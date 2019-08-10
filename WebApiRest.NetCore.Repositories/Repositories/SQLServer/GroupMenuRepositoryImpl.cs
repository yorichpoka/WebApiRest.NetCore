using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiRest.NetCore.Domain.Interfaces.Repositories;
using WebApiRest.NetCore.Domain.Models;
using WebApiRest.NetCore.Repositories;
using WebApiRest.NetCore.Repositories.Contexts;
using WebApiRest.NetCore.Repositories.Entities.SQLServer;

namespace WebApirest.NetCore.Repositories.SQLServer
{
    public class GroupMenuRepositoryImpl : IGroupMenuRepository
    {
        private readonly DataBaseSQLServerContext _DataBaseSQLServerContext;
        private readonly IMapper _Mapper;

        public GroupMenuRepositoryImpl(DataBaseSQLServerContext dataBaseSQLServerContext, IMapper mapper)
        {
            this._DataBaseSQLServerContext = dataBaseSQLServerContext;
            this._Mapper = mapper;
        }

        public Task<GroupMenuModel> Create(GroupMenuModel obj)
        {
            return
                Task.Factory.StartNew<GroupMenuModel>(() =>
                {
                    var value = this._Mapper.Map<GroupMenuModel, GroupMenu>(obj);

                    this._DataBaseSQLServerContext.GroupMenus.Add(value);

                    this._DataBaseSQLServerContext.SaveChanges();

                    return this._Mapper.Map<GroupMenu, GroupMenuModel>(value);
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

                    return this._Mapper.Map<GroupMenu, GroupMenuModel>(value.Result);
                });
        }

        public Task<IEnumerable<GroupMenuModel>> Read()
        {
            return
                Task.Factory.StartNew<IEnumerable<GroupMenuModel>>(() =>
                {
                    var value = this._DataBaseSQLServerContext.GroupMenus.ToList();

                    return this._Mapper.Map<IEnumerable<GroupMenu>, IEnumerable<GroupMenuModel>>(value);
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