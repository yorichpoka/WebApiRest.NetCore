using Microsoft.AspNetCore.Http;
using System.Net;
using WebApiRest.NetCore.Domain.Models;
using WebApiRest.NetCore.Repositories.Entities.MySQL;
using WebApiRest.NetCore.Repositories.Entities.SQLServer;
using Authorization = WebApiRest.NetCore.Repositories.Entities.SQLServer.Authorization;

namespace WebApiRest.NetCore.Tools
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

        public static int ExtConvertToInt32(this string value)
        {
            return
              int.Parse(value);
        }

        public static void ExtAddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Expose-Header", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }

        public static int ExtConvertToInt32(this HttpStatusCode value)
        {
            return (int)value;
        }
    }
}
