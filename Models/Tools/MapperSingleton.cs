using AutoMapper;
using WebApiRest.NetCore.Models.Dtos;

namespace WebApiRest.NetCore.Models.Tools
{
  public sealed class MapperSingleton
  {
    private static IMapper instanceMapper = null;
    private static readonly object padlock = new object();

    private MapperSingleton() { }

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

                cfg.CreateMap<WebApiRest.NetCore.Models.Entity.SQLServer.Authorization, AuthorizationDto>()
                    .ForMember(destination => destination.IdRole, opts => opts.MapFrom(source => source.IdRole))
                    .ForMember(destination => destination.IdMenu, opts => opts.MapFrom(source => source.IdMenu))
                    .ForMember(destination => destination.Create, opts => opts.MapFrom(source => source.Create))
                    .ForMember(destination => destination.Read, opts => opts.MapFrom(source => source.Read))
                    .ForMember(destination => destination.Update, opts => opts.MapFrom(source => source.Update))
                    .ForMember(destination => destination.Delete, opts => opts.MapFrom(source => source.Delete))
                    .ForMember(destination => destination.CreationDate, opts => opts.MapFrom(source => source.CreationDate));
                cfg.CreateMap<AuthorizationDto, WebApiRest.NetCore.Models.Entity.SQLServer.Authorization>()
                    .ForMember(destination => destination.IdRole, opts => opts.MapFrom(source => source.IdRole))
                    .ForMember(destination => destination.IdMenu, opts => opts.MapFrom(source => source.IdMenu))
                    .ForMember(destination => destination.Create, opts => opts.MapFrom(source => source.Create))
                    .ForMember(destination => destination.Read, opts => opts.MapFrom(source => source.Read))
                    .ForMember(destination => destination.Update, opts => opts.MapFrom(source => source.Update))
                    .ForMember(destination => destination.Delete, opts => opts.MapFrom(source => source.Delete))
                    .ForMember(destination => destination.CreationDate, opts => opts.MapFrom(source => source.CreationDate));

                cfg.CreateMap<WebApiRest.NetCore.Models.Entity.MySQL.TblAuthorization, AuthorizationDto>()
                    .ForMember(destination => destination.IdRole, opts => opts.MapFrom(source => source.IdRole))
                    .ForMember(destination => destination.IdMenu, opts => opts.MapFrom(source => source.IdMenu))
                    .ForMember(destination => destination.Create, opts => opts.MapFrom(source => source.Create))
                    .ForMember(destination => destination.Read, opts => opts.MapFrom(source => source.Read))
                    .ForMember(destination => destination.Update, opts => opts.MapFrom(source => source.Update))
                    .ForMember(destination => destination.Delete, opts => opts.MapFrom(source => source.Delete))
                    .ForMember(destination => destination.CreationDate, opts => opts.MapFrom(source => source.CreationDate));
                cfg.CreateMap<AuthorizationDto, WebApiRest.NetCore.Models.Entity.MySQL.TblAuthorization>()
                    .ForMember(destination => destination.IdRole, opts => opts.MapFrom(source => source.IdRole))
                    .ForMember(destination => destination.IdMenu, opts => opts.MapFrom(source => source.IdMenu))
                    .ForMember(destination => destination.Create, opts => opts.MapFrom(source => source.Create))
                    .ForMember(destination => destination.Read, opts => opts.MapFrom(source => source.Read))
                    .ForMember(destination => destination.Update, opts => opts.MapFrom(source => source.Update))
                    .ForMember(destination => destination.Delete, opts => opts.MapFrom(source => source.Delete))
                    .ForMember(destination => destination.CreationDate, opts => opts.MapFrom(source => source.CreationDate));

                #endregion Authorization

                #region Role

                cfg.CreateMap<Entity.SQLServer.Role, RoleDto>()
                    .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                    .ForMember(destination => destination.Title, opts => opts.MapFrom(source => source.Title));
                cfg.CreateMap<RoleDto, Entity.SQLServer.Role>()
                    .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                    .ForMember(destination => destination.Title, opts => opts.MapFrom(source => source.Title));

                cfg.CreateMap<WebApiRest.NetCore.Models.Entity.MySQL.TblRole, RoleDto>()
                    .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                    .ForMember(destination => destination.Title, opts => opts.MapFrom(source => source.Title));
                cfg.CreateMap<RoleDto, WebApiRest.NetCore.Models.Entity.MySQL.TblRole>()
                    .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                    .ForMember(destination => destination.Title, opts => opts.MapFrom(source => source.Title));

                #endregion Role

                #region User

                cfg.CreateMap<Entity.SQLServer.User, UserDto>()
                    .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                    .ForMember(destination => destination.IdRole, opts => opts.MapFrom(source => source.IdRole))
                    .ForMember(destination => destination.Login, opts => opts.MapFrom(source => source.Login))
                    .ForMember(destination => destination.Password, opts => opts.MapFrom(source => source.Password))
                    .ForMember(destination => destination.Name, opts => opts.MapFrom(source => source.Name));
                cfg.CreateMap<UserDto, Entity.SQLServer.User>()
                    .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                    .ForMember(destination => destination.IdRole, opts => opts.MapFrom(source => source.IdRole))
                    .ForMember(destination => destination.Login, opts => opts.MapFrom(source => source.Login))
                    .ForMember(destination => destination.Password, opts => opts.MapFrom(source => source.Password))
                    .ForMember(destination => destination.Name, opts => opts.MapFrom(source => source.Name));

                cfg.CreateMap<WebApiRest.NetCore.Models.Entity.MySQL.TblUser, UserDto>()
                    .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                    .ForMember(destination => destination.IdRole, opts => opts.MapFrom(source => source.IdRole))
                    .ForMember(destination => destination.Login, opts => opts.MapFrom(source => source.Login))
                    .ForMember(destination => destination.Password, opts => opts.MapFrom(source => source.Password))
                    .ForMember(destination => destination.Name, opts => opts.MapFrom(source => source.Name));
                cfg.CreateMap<UserDto, WebApiRest.NetCore.Models.Entity.MySQL.TblUser>()
                    .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                    .ForMember(destination => destination.IdRole, opts => opts.MapFrom(source => source.IdRole))
                    .ForMember(destination => destination.Login, opts => opts.MapFrom(source => source.Login))
                    .ForMember(destination => destination.Password, opts => opts.MapFrom(source => source.Password))
                    .ForMember(destination => destination.Name, opts => opts.MapFrom(source => source.Name));

                #endregion User

                #region GroupMenu

                cfg.CreateMap<Entity.SQLServer.GroupMenu, GroupMenuDto>()
                    .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                    .ForMember(destination => destination.Title, opts => opts.MapFrom(source => source.Title));
                cfg.CreateMap<GroupMenuDto, Entity.SQLServer.GroupMenu>()
                    .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                    .ForMember(destination => destination.Title, opts => opts.MapFrom(source => source.Title));

                cfg.CreateMap<WebApiRest.NetCore.Models.Entity.MySQL.TblGroupMenu, GroupMenuDto>()
                    .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                    .ForMember(destination => destination.Title, opts => opts.MapFrom(source => source.Title));
                cfg.CreateMap<GroupMenuDto, WebApiRest.NetCore.Models.Entity.MySQL.TblGroupMenu>()
                    .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                    .ForMember(destination => destination.Title, opts => opts.MapFrom(source => source.Title));

                #endregion GroupMenu

                #region Menu

                cfg.CreateMap<Entity.SQLServer.Menu, MenuDto>()
                    .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                    .ForMember(destination => destination.IdGroupMenu, opts => opts.MapFrom(source => source.IdGroupMenu))
                    .ForMember(destination => destination.Title, opts => opts.MapFrom(source => source.Title));
                cfg.CreateMap<MenuDto, Entity.SQLServer.Menu>()
                    .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                    .ForMember(destination => destination.IdGroupMenu, opts => opts.MapFrom(source => source.IdGroupMenu))
                    .ForMember(destination => destination.Title, opts => opts.MapFrom(source => source.Title));

                cfg.CreateMap<WebApiRest.NetCore.Models.Entity.MySQL.TblMenu, MenuDto>()
                    .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                    .ForMember(destination => destination.IdGroupMenu, opts => opts.MapFrom(source => source.IdGroupMenu))
                    .ForMember(destination => destination.Title, opts => opts.MapFrom(source => source.Title));
                cfg.CreateMap<MenuDto, WebApiRest.NetCore.Models.Entity.MySQL.TblMenu>()
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
