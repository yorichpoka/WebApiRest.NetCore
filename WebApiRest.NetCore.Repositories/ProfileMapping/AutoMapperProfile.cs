using AutoMapper;
using System.Linq;
using System;
using WebApiRest.NetCore.Domain;
using WebApiRest.NetCore.Domain.Models;
using WebApiRest.NetCore.Domain.Models.MongoDB;
using WebApiRest.NetCore.Domain.Models.SqLite;
using WebApiRest.NetCore.Repositories.Entities.MongoDB;
using WebApiRest.NetCore.Repositories.Entities.SqLite;
using MySqlPkg = WebApiRest.NetCore.Repositories.Entities.MySql;
using SqlServerPkg = WebApiRest.NetCore.Repositories.Entities.SqlServer;

namespace WebApiRest.NetCore.Repositories.ProfileMapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Authorization

            CreateMap<SqlServerPkg.Authorization, AuthorizationModel>()
                .ForMember(destination => destination.IdRole, opts => opts.MapFrom(source => source.IdRole))
                .ForMember(destination => destination.IdMenu, opts => opts.MapFrom(source => source.IdMenu))
                .ForMember(destination => destination.Create, opts => opts.MapFrom(source => source.Create))
                .ForMember(destination => destination.Read, opts => opts.MapFrom(source => source.Read))
                .ForMember(destination => destination.Update, opts => opts.MapFrom(source => source.Update))
                .ForMember(destination => destination.Delete, opts => opts.MapFrom(source => source.Delete))
                .ForMember(destination => destination.CreationDate, opts => opts.MapFrom(source => source.CreationDate));
            CreateMap<AuthorizationModel, SqlServerPkg.Authorization>()
                .ForMember(destination => destination.IdRole, opts => opts.MapFrom(source => source.IdRole))
                .ForMember(destination => destination.IdMenu, opts => opts.MapFrom(source => source.IdMenu))
                .ForMember(destination => destination.Create, opts => opts.MapFrom(source => source.Create))
                .ForMember(destination => destination.Read, opts => opts.MapFrom(source => source.Read))
                .ForMember(destination => destination.Update, opts => opts.MapFrom(source => source.Update))
                .ForMember(destination => destination.Delete, opts => opts.MapFrom(source => source.Delete))
                .ForMember(destination => destination.CreationDate, opts => opts.MapFrom(source => source.CreationDate));

            CreateMap<MySqlPkg.Authorization, AuthorizationModel>()
                .ForMember(destination => destination.IdRole, opts => opts.MapFrom(source => source.IdRole))
                .ForMember(destination => destination.IdMenu, opts => opts.MapFrom(source => source.IdMenu))
                .ForMember(destination => destination.Create, opts => opts.MapFrom(source => source.Create))
                .ForMember(destination => destination.Read, opts => opts.MapFrom(source => source.Read))
                .ForMember(destination => destination.Update, opts => opts.MapFrom(source => source.Update))
                .ForMember(destination => destination.Delete, opts => opts.MapFrom(source => source.Delete))
                .ForMember(destination => destination.CreationDate, opts => opts.MapFrom(source => source.CreationDate));
            CreateMap<AuthorizationModel, MySqlPkg.Authorization>()
                .ForMember(destination => destination.IdRole, opts => opts.MapFrom(source => source.IdRole))
                .ForMember(destination => destination.IdMenu, opts => opts.MapFrom(source => source.IdMenu))
                .ForMember(destination => destination.Create, opts => opts.MapFrom(source => source.Create))
                .ForMember(destination => destination.Read, opts => opts.MapFrom(source => source.Read))
                .ForMember(destination => destination.Update, opts => opts.MapFrom(source => source.Update))
                .ForMember(destination => destination.Delete, opts => opts.MapFrom(source => source.Delete))
                .ForMember(destination => destination.CreationDate, opts => opts.MapFrom(source => source.CreationDate));

            #endregion Authorization

            #region Role

            CreateMap<SqlServerPkg.Role, RoleModel>()
                .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                .ForMember(destination => destination.Title, opts => opts.MapFrom(source => source.Title))
                .ForMember(destination => destination.IdUsers, opts => opts.MapFrom(source => source.Users.Select(l => l.Id)));
            CreateMap<RoleModel, SqlServerPkg.Role>()
                .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                .ForMember(destination => destination.Title, opts => opts.MapFrom(source => source.Title));

            CreateMap<MySqlPkg.Role, RoleModel>()
                .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                .ForMember(destination => destination.Title, opts => opts.MapFrom(source => source.Title))
                .ForMember(destination => destination.IdUsers, opts => opts.MapFrom(source => source.Users.Select(l => l.Id)));
            CreateMap<RoleModel, MySqlPkg.Role>()
                .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                .ForMember(destination => destination.Title, opts => opts.MapFrom(source => source.Title));

            #endregion Role

            #region User

            CreateMap<SqlServerPkg.User, UserModel>()
                .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                .ForMember(destination => destination.IdRole, opts => opts.MapFrom(source => source.IdRole))
                .ForMember(destination => destination.Login, opts => opts.MapFrom(source => source.Login))
                .ForMember(destination => destination.Password, opts => opts.MapFrom(source => source.Password))
                .ForMember(destination => destination.Name, opts => opts.MapFrom(source => source.Name));
            CreateMap<UserModel, SqlServerPkg.User>()
                .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                .ForMember(destination => destination.IdRole, opts => opts.MapFrom(source => source.IdRole))
                .ForMember(destination => destination.Login, opts => opts.MapFrom(source => source.Login))
                .ForMember(destination => destination.Password, opts => opts.MapFrom(source => source.Password))
                .ForMember(destination => destination.Name, opts => opts.MapFrom(source => source.Name));

            CreateMap<MySqlPkg.User, UserModel>()
                .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                .ForMember(destination => destination.IdRole, opts => opts.MapFrom(source => source.IdRole))
                .ForMember(destination => destination.Login, opts => opts.MapFrom(source => source.Login))
                .ForMember(destination => destination.Password, opts => opts.MapFrom(source => source.Password))
                .ForMember(destination => destination.Name, opts => opts.MapFrom(source => source.Name));
            CreateMap<UserModel, MySqlPkg.User>()
                .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                .ForMember(destination => destination.IdRole, opts => opts.MapFrom(source => source.IdRole))
                .ForMember(destination => destination.Login, opts => opts.MapFrom(source => source.Login))
                .ForMember(destination => destination.Password, opts => opts.MapFrom(source => source.Password))
                .ForMember(destination => destination.Name, opts => opts.MapFrom(source => source.Name));

            CreateMap<SqlServerPkg.User, UserRoleModel>()
                .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                .ForMember(destination => destination.IdRole, opts => opts.MapFrom(source => source.IdRole))
                .ForMember(destination => destination.Login, opts => opts.MapFrom(source => source.Login))
                .ForMember(destination => destination.Password, opts => opts.MapFrom(source => source.Password))
                .ForMember(destination => destination.Name, opts => opts.MapFrom(source => source.Name))
                .ForMember(destination => destination.Role, opts => opts.MapFrom(source => source.Role));

            CreateMap<MySqlPkg.User, UserRoleModel>()
                .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                .ForMember(destination => destination.IdRole, opts => opts.MapFrom(source => source.IdRole))
                .ForMember(destination => destination.Login, opts => opts.MapFrom(source => source.Login))
                .ForMember(destination => destination.Password, opts => opts.MapFrom(source => source.Password))
                .ForMember(destination => destination.Name, opts => opts.MapFrom(source => source.Name))
                .ForMember(destination => destination.Role, opts => opts.MapFrom(source => source.Role));

            #endregion User

            #region GroupMenu

            CreateMap<SqlServerPkg.GroupMenu, GroupMenuModel>()
                .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                .ForMember(destination => destination.Title, opts => opts.MapFrom(source => source.Title))
                .ForMember(destination => destination.IdMenus, opts => opts.MapFrom(source => source.Menus.Select(l => l.Id)));
            CreateMap<GroupMenuModel, SqlServerPkg.GroupMenu>()
                .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                .ForMember(destination => destination.Title, opts => opts.MapFrom(source => source.Title));

            CreateMap<MySqlPkg.GroupMenu, GroupMenuModel>()
                .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                .ForMember(destination => destination.Title, opts => opts.MapFrom(source => source.Title))
                .ForMember(destination => destination.IdMenus, opts => opts.MapFrom(source => source.Menus.Select(l => l.Id)));
            CreateMap<GroupMenuModel, MySqlPkg.GroupMenu>()
                .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                .ForMember(destination => destination.Title, opts => opts.MapFrom(source => source.Title));

            #endregion GroupMenu

            #region Menu

            CreateMap<SqlServerPkg.Menu, MenuModel>()
                .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                .ForMember(destination => destination.IdGroupMenu, opts => opts.MapFrom(source => source.IdGroupMenu))
                .ForMember(destination => destination.Title, opts => opts.MapFrom(source => source.Title));
            CreateMap<MenuModel, SqlServerPkg.Menu>()
                .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                .ForMember(destination => destination.IdGroupMenu, opts => opts.MapFrom(source => source.IdGroupMenu))
                .ForMember(destination => destination.Title, opts => opts.MapFrom(source => source.Title));

            CreateMap<MySqlPkg.Menu, MenuModel>()
                .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                .ForMember(destination => destination.IdGroupMenu, opts => opts.MapFrom(source => source.IdGroupMenu))
                .ForMember(destination => destination.Title, opts => opts.MapFrom(source => source.Title));
            CreateMap<MenuModel, MySqlPkg.Menu>()
                .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                .ForMember(destination => destination.IdGroupMenu, opts => opts.MapFrom(source => source.IdGroupMenu))
                .ForMember(destination => destination.Title, opts => opts.MapFrom(source => source.Title));

            #endregion Menu

            #region MongoDB

            CreateMap<Address, AddressModel>();
            CreateMap<AddressModel, Address>();

            CreateMap<Coordinate, CoordinateModel>()
                .ForMember(destination => destination.Coordinate, opts => opts.MapFrom(source => source.Coordinates.ExtConvertToCoordinateStruct()));
            CreateMap<CoordinateModel, Coordinate>()
                .ForMember(destination => destination.Coordinates, opts => opts.MapFrom(source => source.Coordinate.ExtConvertToArray()));

            CreateMap<Grade, GradeModel>();
            CreateMap<GradeModel, Grade>();

            CreateMap<Restaurant, RestaurantModel>();
            CreateMap<RestaurantModel, Restaurant>();

            CreateMap<Document, DocumentModel>();
            CreateMap<DocumentModel, Document>();

            #endregion MongoDB

            #region SqLite

            CreateMap<WebSite, WebSiteModel>()
                .ForMember(destination => destination.Created, opts => opts.MapFrom(source => source.Created.ExtConvertToDateTime()))
                .ForMember(destination => destination.LastModified, opts => opts.MapFrom(source => source.LastModified.ExtConvertToDateTime()))
                .ForMember(destination => destination.MetaDataModified, opts => opts.MapFrom(source => source.MetaDataModified.ExtConvertToDateTime()))
                .ForMember(destination => destination.Location, opts => opts.MapFrom(source => source.Location.ExtConvertToCoordinateStruct()));
            CreateMap<WebSiteModel, WebSite>()
                .ForMember(destination => destination.Created, opts => opts.MapFrom(source => (source.Created.HasValue  ? source.Created.Value 
                                                                                                                        : DateTime.MinValue).ToString("s")))
                .ForMember(destination => destination.LastModified, opts => opts.MapFrom(source => (source.LastModified.HasValue    ? source.LastModified.Value 
                                                                                                                                    : DateTime.MinValue).ToString("s")))
                .ForMember(destination => destination.MetaDataModified, opts => opts.MapFrom(source => (source.MetaDataModified.HasValue    ? source.MetaDataModified.Value 
                                                                                                                                            : DateTime.MinValue).ToString("s")))
                .ForMember(destination => destination.Location, opts => opts.MapFrom(source => source.Location.ExtConvertToString()));

            #endregion
        }
    }
}