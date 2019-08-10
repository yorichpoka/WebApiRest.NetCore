using AutoMapper;
using WebApiRest.NetCore.Domain.Models;
using WebApiRest.NetCore.Repositories.Entities.MySQL;
using WebApiRest.NetCore.Repositories.Entities.SQLServer;

namespace WebApiRest.NetCore.Repositories.ProfileMapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Authorization

            CreateMap<Authorization, AuthorizationModel>()
                .ForMember(destination => destination.IdRole, opts => opts.MapFrom(source => source.IdRole))
                .ForMember(destination => destination.IdMenu, opts => opts.MapFrom(source => source.IdMenu))
                .ForMember(destination => destination.Create, opts => opts.MapFrom(source => source.Create))
                .ForMember(destination => destination.Read, opts => opts.MapFrom(source => source.Read))
                .ForMember(destination => destination.Update, opts => opts.MapFrom(source => source.Update))
                .ForMember(destination => destination.Delete, opts => opts.MapFrom(source => source.Delete))
                .ForMember(destination => destination.CreationDate, opts => opts.MapFrom(source => source.CreationDate));
            CreateMap<AuthorizationModel, Authorization>()
                .ForMember(destination => destination.IdRole, opts => opts.MapFrom(source => source.IdRole))
                .ForMember(destination => destination.IdMenu, opts => opts.MapFrom(source => source.IdMenu))
                .ForMember(destination => destination.Create, opts => opts.MapFrom(source => source.Create))
                .ForMember(destination => destination.Read, opts => opts.MapFrom(source => source.Read))
                .ForMember(destination => destination.Update, opts => opts.MapFrom(source => source.Update))
                .ForMember(destination => destination.Delete, opts => opts.MapFrom(source => source.Delete))
                .ForMember(destination => destination.CreationDate, opts => opts.MapFrom(source => source.CreationDate));

            CreateMap<TblAuthorization, AuthorizationModel>()
                .ForMember(destination => destination.IdRole, opts => opts.MapFrom(source => source.IdRole))
                .ForMember(destination => destination.IdMenu, opts => opts.MapFrom(source => source.IdMenu))
                .ForMember(destination => destination.Create, opts => opts.MapFrom(source => source.Create))
                .ForMember(destination => destination.Read, opts => opts.MapFrom(source => source.Read))
                .ForMember(destination => destination.Update, opts => opts.MapFrom(source => source.Update))
                .ForMember(destination => destination.Delete, opts => opts.MapFrom(source => source.Delete))
                .ForMember(destination => destination.CreationDate, opts => opts.MapFrom(source => source.CreationDate));
            CreateMap<AuthorizationModel, TblAuthorization>()
                .ForMember(destination => destination.IdRole, opts => opts.MapFrom(source => source.IdRole))
                .ForMember(destination => destination.IdMenu, opts => opts.MapFrom(source => source.IdMenu))
                .ForMember(destination => destination.Create, opts => opts.MapFrom(source => source.Create))
                .ForMember(destination => destination.Read, opts => opts.MapFrom(source => source.Read))
                .ForMember(destination => destination.Update, opts => opts.MapFrom(source => source.Update))
                .ForMember(destination => destination.Delete, opts => opts.MapFrom(source => source.Delete))
                .ForMember(destination => destination.CreationDate, opts => opts.MapFrom(source => source.CreationDate));

            #endregion Authorization

            #region Role

            CreateMap<Role, RoleModel>()
                .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                .ForMember(destination => destination.Title, opts => opts.MapFrom(source => source.Title));
            CreateMap<RoleModel, Role>()
                .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                .ForMember(destination => destination.Title, opts => opts.MapFrom(source => source.Title));

            CreateMap<TblRole, RoleModel>()
                .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                .ForMember(destination => destination.Title, opts => opts.MapFrom(source => source.Title));
            CreateMap<RoleModel, TblRole>()
                .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                .ForMember(destination => destination.Title, opts => opts.MapFrom(source => source.Title));

            #endregion Role

            #region User

            CreateMap<User, UserModel>()
                .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                .ForMember(destination => destination.IdRole, opts => opts.MapFrom(source => source.IdRole))
                .ForMember(destination => destination.Login, opts => opts.MapFrom(source => source.Login))
                .ForMember(destination => destination.Password, opts => opts.MapFrom(source => source.Password))
                .ForMember(destination => destination.Name, opts => opts.MapFrom(source => source.Name));
            CreateMap<UserModel, User>()
                .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                .ForMember(destination => destination.IdRole, opts => opts.MapFrom(source => source.IdRole))
                .ForMember(destination => destination.Login, opts => opts.MapFrom(source => source.Login))
                .ForMember(destination => destination.Password, opts => opts.MapFrom(source => source.Password))
                .ForMember(destination => destination.Name, opts => opts.MapFrom(source => source.Name));

            CreateMap<TblUser, UserModel>()
                .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                .ForMember(destination => destination.IdRole, opts => opts.MapFrom(source => source.IdRole))
                .ForMember(destination => destination.Login, opts => opts.MapFrom(source => source.Login))
                .ForMember(destination => destination.Password, opts => opts.MapFrom(source => source.Password))
                .ForMember(destination => destination.Name, opts => opts.MapFrom(source => source.Name));
            CreateMap<UserModel, TblUser>()
                .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                .ForMember(destination => destination.IdRole, opts => opts.MapFrom(source => source.IdRole))
                .ForMember(destination => destination.Login, opts => opts.MapFrom(source => source.Login))
                .ForMember(destination => destination.Password, opts => opts.MapFrom(source => source.Password))
                .ForMember(destination => destination.Name, opts => opts.MapFrom(source => source.Name));

            CreateMap<User, UserRoleModel>()
                .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                .ForMember(destination => destination.IdRole, opts => opts.MapFrom(source => source.IdRole))
                .ForMember(destination => destination.Login, opts => opts.MapFrom(source => source.Login))
                .ForMember(destination => destination.Password, opts => opts.MapFrom(source => source.Password))
                .ForMember(destination => destination.Name, opts => opts.MapFrom(source => source.Name))
                .ForMember(destination => destination.Role, opts => opts.MapFrom(source => source.Role));

            CreateMap<TblUser, UserRoleModel>()
                .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                .ForMember(destination => destination.IdRole, opts => opts.MapFrom(source => source.IdRole))
                .ForMember(destination => destination.Login, opts => opts.MapFrom(source => source.Login))
                .ForMember(destination => destination.Password, opts => opts.MapFrom(source => source.Password))
                .ForMember(destination => destination.Name, opts => opts.MapFrom(source => source.Name))
                .ForMember(destination => destination.Role, opts => opts.MapFrom(source => source.Role));

            #endregion User

            #region GroupMenu

            CreateMap<GroupMenu, GroupMenuModel>()
                .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                .ForMember(destination => destination.Title, opts => opts.MapFrom(source => source.Title));
            CreateMap<GroupMenuModel, GroupMenu>()
                .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                .ForMember(destination => destination.Title, opts => opts.MapFrom(source => source.Title));

            CreateMap<TblGroupMenu, GroupMenuModel>()
                .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                .ForMember(destination => destination.Title, opts => opts.MapFrom(source => source.Title));
            CreateMap<GroupMenuModel, TblGroupMenu>()
                .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                .ForMember(destination => destination.Title, opts => opts.MapFrom(source => source.Title));

            #endregion GroupMenu

            #region Menu

            CreateMap<Menu, MenuModel>()
                .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                .ForMember(destination => destination.IdGroupMenu, opts => opts.MapFrom(source => source.IdGroupMenu))
                .ForMember(destination => destination.Title, opts => opts.MapFrom(source => source.Title));
            CreateMap<MenuModel, Menu>()
                .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                .ForMember(destination => destination.IdGroupMenu, opts => opts.MapFrom(source => source.IdGroupMenu))
                .ForMember(destination => destination.Title, opts => opts.MapFrom(source => source.Title));

            CreateMap<TblMenu, MenuModel>()
                .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                .ForMember(destination => destination.IdGroupMenu, opts => opts.MapFrom(source => source.IdGroupMenu))
                .ForMember(destination => destination.Title, opts => opts.MapFrom(source => source.Title));
            CreateMap<MenuModel, TblMenu>()
                .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                .ForMember(destination => destination.IdGroupMenu, opts => opts.MapFrom(source => source.IdGroupMenu))
                .ForMember(destination => destination.Title, opts => opts.MapFrom(source => source.Title));

            #endregion Menu
        }
    }
}