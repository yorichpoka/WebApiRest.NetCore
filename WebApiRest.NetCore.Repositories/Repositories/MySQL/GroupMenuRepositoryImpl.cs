﻿using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiRest.NetCore.Domain.Interfaces.Repositories;
using WebApiRest.NetCore.Domain.Models;
using WebApiRest.NetCore.Repositories.Contexts;
using WebApiRest.NetCore.Repositories.Entities.MySQL;

namespace WebApiRest.NetCore.Repositories.MySQL
{
    public class GroupMenuRepositoryImpl : IGroupMenuRepository
    {
        private readonly DataBaseMySQLContext _DataBaseMySQLContext;
        private readonly IMapper _Mapper;

        public GroupMenuRepositoryImpl(DataBaseMySQLContext dataBaseMySQLContext, IMapper mapper)
        {
            this._DataBaseMySQLContext = dataBaseMySQLContext;
            this._Mapper = mapper;
        }

        public Task<GroupMenuModel> Create(GroupMenuModel obj)
        {
            return
                Task.Factory.StartNew<GroupMenuModel>(() =>
                {
                    var value = this._Mapper.Map<GroupMenuModel, TblGroupMenu>(obj);

                    this._DataBaseMySQLContext.GroupMenus.Add(value);

                    this._DataBaseMySQLContext.SaveChanges();

                    return this._Mapper.Map<TblGroupMenu, GroupMenuModel>(value);
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

        public Task<GroupMenuModel> Read(int id)
        {
            return
                Task.Factory.StartNew<GroupMenuModel>(() =>
                {
                    var value = this._DataBaseMySQLContext.GroupMenus.FindAsync(id);

                    return this._Mapper.Map<TblGroupMenu, GroupMenuModel>(value.Result);
                });
        }

        public Task<IEnumerable<GroupMenuModel>> Read()
        {
            return
                Task.Factory.StartNew<IEnumerable<GroupMenuModel>>(() =>
                {
                    var value = this._DataBaseMySQLContext.GroupMenus.ToList();

                    return this._Mapper.Map<IEnumerable<TblGroupMenu>, IEnumerable<GroupMenuModel>>(value);
                });
        }

        public Task Update(GroupMenuModel obj)
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