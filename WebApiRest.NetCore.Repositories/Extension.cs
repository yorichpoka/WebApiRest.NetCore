using WebApiRest.NetCore.Domain.Models;
using WebApiRest.NetCore.Repositories.Entities.MySQL;
using WebApiRest.NetCore.Repositories.Entities.SQLServer;

namespace WebApiRest.NetCore.Repositories
{
    public static class Extension
    {
        #region GroupMenu

        public static void ExtUpdate(this GroupMenu value, GroupMenuModel obj)
        {
            value.Title = obj.Title;
        }

        public static void ExtUpdate(this TblGroupMenu value, GroupMenuModel obj)
        {
            value.Title = obj.Title;
        }

        #endregion GroupMenu

        #region Authorization

        public static void ExtUpdate(this Authorization value, AuthorizationModel obj)
        {
            value.Create = obj.Create;
            value.Read = obj.Read;
            value.Update = obj.Update;
            value.Delete = obj.Delete;
            value.CreationDate = obj.CreationDate;
        }

        public static void ExtUpdate(this TblAuthorization value, AuthorizationModel obj)
        {
            value.Create = obj.Create;
            value.Read = obj.Read;
            value.Update = obj.Update;
            value.Delete = obj.Delete;
            value.CreationDate = obj.CreationDate;
        }

        #endregion Authorization

        #region User

        public static void ExtUpdate(this User value, UserModel obj)
        {
            value.IdRole = obj.IdRole;
            value.Login = obj.Login;
            value.Password = obj.Password;
            value.Name = obj.Name;
        }

        public static void ExtUpdate(this TblUser value, UserModel obj)
        {
            value.IdRole = obj.IdRole;
            value.Login = obj.Login;
            value.Password = obj.Password;
            value.Name = obj.Name;
        }

        #endregion User

        #region Role

        public static void ExtUpdate(this Role value, RoleModel obj)
        {
            value.Title = obj.Title;
        }

        public static void ExtUpdate(this TblRole value, RoleModel obj)
        {
            value.Title = obj.Title;
        }

        #endregion Role

        #region Menu

        public static void ExtUpdate(this Menu value, MenuModel obj)
        {
            value.Title = obj.Title;
            value.IdGroupMenu = obj.IdGroupMenu;
        }

        public static void ExtUpdate(this TblMenu value, MenuModel obj)
        {
            value.Title = obj.Title;
            value.IdGroupMenu = obj.IdGroupMenu;
        }

        #endregion Menu
    }
}