using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiRest.NetCore.Domain.Interfaces.Repositories;
using WebApiRest.NetCore.Domain.Models;
using WebApiRest.NetCore.Repositories.Contexts;
using WebApiRest.NetCore.Repositories.Entities.MySQL;

namespace WebApiRest.NetCore.Repositories.MySQL
{
    public class RoleRepositoryImpl : IRoleRepository
    {
        private readonly DataBaseMySQLContext _DataBaseMySQLContext;
        private readonly IMapper _Mapper;

        public RoleRepositoryImpl(DataBaseMySQLContext dataBaseMySQLContext, IMapper mapper)
        {
            this._DataBaseMySQLContext = dataBaseMySQLContext;
            this._Mapper = mapper;
        }

        public Task<RoleModel> Create(RoleModel obj)
        {
            return
                Task.Factory.StartNew<RoleModel>(() =>
                {
                    var value = this._Mapper.Map<RoleModel, TblRole>(obj);

                    this._DataBaseMySQLContext.Roles.Add(value);

                    this._DataBaseMySQLContext.SaveChanges();

                    return this._Mapper.Map<TblRole, RoleModel>(value);
                });
        }

        public Task Delete(int id)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    this._DataBaseMySQLContext.Roles.Remove(
                        this._DataBaseMySQLContext.Roles.Find(id)
                    );

                    this._DataBaseMySQLContext.SaveChanges();
                });
        }

        public Task<RoleModel> Read(int id)
        {
            return
                Task.Factory.StartNew<RoleModel>(() =>
                {
                    var value = this._DataBaseMySQLContext.Roles.FindAsync(id);

                    return this._Mapper.Map<TblRole, RoleModel>(value.Result);
                });
        }

        public Task<IEnumerable<RoleModel>> Read()
        {
            return
                Task.Factory.StartNew<IEnumerable<RoleModel>>(() =>
                {
                    var value = this._DataBaseMySQLContext.Roles.ToList();

                    return this._Mapper.Map<IEnumerable<TblRole>, IEnumerable<RoleModel>>(value);
                });
        }

        public Task Update(RoleModel obj)
        {
            return
                Task.Factory.StartNew(() =>
                {
                    var value = this._DataBaseMySQLContext.Roles.Find(obj.Id);

                    value.ExtUpdate(obj);

                    this._DataBaseMySQLContext.Roles.Update(value);

                    this._DataBaseMySQLContext.SaveChanges();
                });
        }
    }
}