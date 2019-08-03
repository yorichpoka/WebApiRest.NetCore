using AutoMapper;
using WebApiRest.NetCore.Domain.Models;
using WebApiRest.NetCore.Repositories.Entities.MySQL;
using WebApiRest.NetCore.Repositories.Entities.SQLServer;

namespace WebApiRest.NetCore.Tools
{
    public sealed class MapperSingleton
    {
        private static IMapper instanceMapper = null;
        private static readonly object padlock = new object();

        private MapperSingleton()
        {
        }

        public static IMapper Instance
        {
            get {
                lock (padlock)
                {
                    if (instanceMapper == null)
                    {
                        instanceMapper = new MapperConfiguration(cfg =>
                        {
                #region Authorization

                cfg.CreateMap<Authorization, AuthorizationModel>()
                                .ForMember(destination => destination.IdRole, opts => opts.MapFrom(source => source.IdRole))
                                .ForMember(destination => destination.IdMenu, opts => opts.MapFrom(source => source.IdMenu))
                                .ForMember(destination => destination.Create, opts => opts.MapFrom(source => source.Create))
                                .ForMember(destination => destination.Read, opts => opts.MapFrom(source => source.Read))
                                .ForMember(destination => destination.Update, opts => opts.MapFrom(source => source.Update))
                                .ForMember(destination => destination.Delete, opts => opts.MapFrom(source => source.Delete))
                                .ForMember(destination => destination.CreationDate, opts => opts.MapFrom(source => source.CreationDate));
                            cfg.CreateMap<AuthorizationModel, Authorization>()
                                .ForMember(destination => destination.IdRole, opts => opts.MapFrom(source => source.IdRole))
                                .ForMember(destination => destination.IdMenu, opts => opts.MapFrom(source => source.IdMenu))
                                .ForMember(destination => destination.Create, opts => opts.MapFrom(source => source.Create))
                                .ForMember(destination => destination.Read, opts => opts.MapFrom(source => source.Read))
                                .ForMember(destination => destination.Update, opts => opts.MapFrom(source => source.Update))
                                .ForMember(destination => destination.Delete, opts => opts.MapFrom(source => source.Delete))
                                .ForMember(destination => destination.CreationDate, opts => opts.MapFrom(source => source.CreationDate));

                            cfg.CreateMap<TblAuthorization, AuthorizationModel>()
                                .ForMember(destination => destination.IdRole, opts => opts.MapFrom(source => source.IdRole))
                                .ForMember(destination => destination.IdMenu, opts => opts.MapFrom(source => source.IdMenu))
                                .ForMember(destination => destination.Create, opts => opts.MapFrom(source => source.Create))
                                .ForMember(destination => destination.Read, opts => opts.MapFrom(source => source.Read))
                                .ForMember(destination => destination.Update, opts => opts.MapFrom(source => source.Update))
                                .ForMember(destination => destination.Delete, opts => opts.MapFrom(source => source.Delete))
                                .ForMember(destination => destination.CreationDate, opts => opts.MapFrom(source => source.CreationDate));
                            cfg.CreateMap<AuthorizationModel, TblAuthorization>()
                                .ForMember(destination => destination.IdRole, opts => opts.MapFrom(source => source.IdRole))
                                .ForMember(destination => destination.IdMenu, opts => opts.MapFrom(source => source.IdMenu))
                                .ForMember(destination => destination.Create, opts => opts.MapFrom(source => source.Create))
                                .ForMember(destination => destination.Read, opts => opts.MapFrom(source => source.Read))
                                .ForMember(destination => destination.Update, opts => opts.MapFrom(source => source.Update))
                                .ForMember(destination => destination.Delete, opts => opts.MapFrom(source => source.Delete))
                                .ForMember(destination => destination.CreationDate, opts => opts.MapFrom(source => source.CreationDate));

                #endregion Authorization

                #region Role

                cfg.CreateMap<Role, RoleModel>()
                                .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                                .ForMember(destination => destination.Title, opts => opts.MapFrom(source => source.Title));
                            cfg.CreateMap<RoleModel, Role>()
                                .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                                .ForMember(destination => destination.Title, opts => opts.MapFrom(source => source.Title));

                            cfg.CreateMap<TblRole, RoleModel>()
                                .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                                .ForMember(destination => destination.Title, opts => opts.MapFrom(source => source.Title));
                            cfg.CreateMap<RoleModel, TblRole>()
                                .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                                .ForMember(destination => destination.Title, opts => opts.MapFrom(source => source.Title));

                #endregion Role

                #region User

                cfg.CreateMap<User, UserModel>()
                                .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                                .ForMember(destination => destination.IdRole, opts => opts.MapFrom(source => source.IdRole))
                                .ForMember(destination => destination.Login, opts => opts.MapFrom(source => source.Login))
                                .ForMember(destination => destination.Password, opts => opts.MapFrom(source => source.Password))
                                .ForMember(destination => destination.Name, opts => opts.MapFrom(source => source.Name));
                            cfg.CreateMap<UserModel, User>()
                                .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                                .ForMember(destination => destination.IdRole, opts => opts.MapFrom(source => source.IdRole))
                                .ForMember(destination => destination.Login, opts => opts.MapFrom(source => source.Login))
                                .ForMember(destination => destination.Password, opts => opts.MapFrom(source => source.Password))
                                .ForMember(destination => destination.Name, opts => opts.MapFrom(source => source.Name));

                            cfg.CreateMap<TblUser, UserModel>()
                                .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                                .ForMember(destination => destination.IdRole, opts => opts.MapFrom(source => source.IdRole))
                                .ForMember(destination => destination.Login, opts => opts.MapFrom(source => source.Login))
                                .ForMember(destination => destination.Password, opts => opts.MapFrom(source => source.Password))
                                .ForMember(destination => destination.Name, opts => opts.MapFrom(source => source.Name));
                            cfg.CreateMap<UserModel, TblUser>()
                                .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                                .ForMember(destination => destination.IdRole, opts => opts.MapFrom(source => source.IdRole))
                                .ForMember(destination => destination.Login, opts => opts.MapFrom(source => source.Login))
                                .ForMember(destination => destination.Password, opts => opts.MapFrom(source => source.Password))
                                .ForMember(destination => destination.Name, opts => opts.MapFrom(source => source.Name));

                            cfg.CreateMap<User, UserRoleModel>()
                                .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                                .ForMember(destination => destination.IdRole, opts => opts.MapFrom(source => source.IdRole))
                                .ForMember(destination => destination.Login, opts => opts.MapFrom(source => source.Login))
                                .ForMember(destination => destination.Password, opts => opts.MapFrom(source => source.Password))
                                .ForMember(destination => destination.Name, opts => opts.MapFrom(source => source.Name))
                                .ForMember(destination => destination.Role, opts => opts.MapFrom(source => source.Role));

                            cfg.CreateMap<TblUser, UserRoleModel>()
                                .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                                .ForMember(destination => destination.IdRole, opts => opts.MapFrom(source => source.IdRole))
                                .ForMember(destination => destination.Login, opts => opts.MapFrom(source => source.Login))
                                .ForMember(destination => destination.Password, opts => opts.MapFrom(source => source.Password))
                                .ForMember(destination => destination.Name, opts => opts.MapFrom(source => source.Name))
                                .ForMember(destination => destination.Role, opts => opts.MapFrom(source => source.Role));

                #endregion User

                #region GroupMenu

                cfg.CreateMap<GroupMenu, GroupMenuModel>()
                                .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                                .ForMember(destination => destination.Title, opts => opts.MapFrom(source => source.Title));
                            cfg.CreateMap<GroupMenuModel, GroupMenu>()
                                .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                                .ForMember(destination => destination.Title, opts => opts.MapFrom(source => source.Title));

                            cfg.CreateMap<TblGroupMenu, GroupMenuModel>()
                                .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                                .ForMember(destination => destination.Title, opts => opts.MapFrom(source => source.Title));
                            cfg.CreateMap<GroupMenuModel, TblGroupMenu>()
                                .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                                .ForMember(destination => destination.Title, opts => opts.MapFrom(source => source.Title));

                #endregion GroupMenu

                #region Menu

                cfg.CreateMap<Menu, MenuModel>()
                                .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                                .ForMember(destination => destination.IdGroupMenu, opts => opts.MapFrom(source => source.IdGroupMenu))
                                .ForMember(destination => destination.Title, opts => opts.MapFrom(source => source.Title));
                            cfg.CreateMap<MenuModel, Menu>()
                                .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                                .ForMember(destination => destination.IdGroupMenu, opts => opts.MapFrom(source => source.IdGroupMenu))
                                .ForMember(destination => destination.Title, opts => opts.MapFrom(source => source.Title));

                            cfg.CreateMap<TblMenu, MenuModel>()
                                .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                                .ForMember(destination => destination.IdGroupMenu, opts => opts.MapFrom(source => source.IdGroupMenu))
                                .ForMember(destination => destination.Title, opts => opts.MapFrom(source => source.Title));
                            cfg.CreateMap<MenuModel, TblMenu>()
                                .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                                .ForMember(destination => destination.IdGroupMenu, opts => opts.MapFrom(source => source.IdGroupMenu))
                                .ForMember(destination => destination.Title, opts => opts.MapFrom(source => source.Title));

                #endregion Menu
            })
                        .CreateMapper();
                    }

                    return instanceMapper;
                }
            }
        }
    }
}