using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiRest.NetCore.Domain.Interfaces.Repositories;
using WebApiRest.NetCore.Domain.Models;
using WebApiRest.NetCore.Repositories.Contexts;
using SqlServerPkg = WebApiRest.NetCore.Repositories.Entities.SqlServer;

namespace WebApiRest.NetCore.Repositories.Repositories.SqlServer
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
                    var value = this._Mapper.Map<GroupMenuModel, SqlServerPkg.GroupMenu>(obj);

                    this._DataBaseSQLServerContext.GroupMenus.Add(value);

                    this._DataBaseSQLServerContext.SaveChanges();

                    return this._Mapper.Map<SqlServerPkg.GroupMenu, GroupMenuModel>(value);
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

        public Task Delete(int[] ids)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    this._DataBaseSQLServerContext.GroupMenus.RemoveRange(
                        this._DataBaseSQLServerContext.GroupMenus.Where(l => ids.Contains(l.Id))
                    );

                    this._DataBaseSQLServerContext.SaveChanges();
                });
        }

        public Task<GroupMenuModel> Read(int id)
        {
            return
                Task.Factory.StartNew<GroupMenuModel>(() =>
                {
                    var value = this._DataBaseSQLServerContext.GroupMenus.Include(l => l.Menus)
                                                                         .FirstOrDefault(l => l.Id == id);

                    return this._Mapper.Map<SqlServerPkg.GroupMenu, GroupMenuModel>(value);
                });
        }

        public Task<IEnumerable<GroupMenuModel>> Read()
        {
            return
                Task.Factory.StartNew<IEnumerable<GroupMenuModel>>(() =>
                {
                    var value = this._DataBaseSQLServerContext.GroupMenus.Include(l => l.Menus)
                                                                         .ToList();

                    return this._Mapper.Map<IEnumerable<SqlServerPkg.GroupMenu>, IEnumerable<GroupMenuModel>>(value);
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