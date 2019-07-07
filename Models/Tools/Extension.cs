using Microsoft.AspNetCore.Http;
using System.Net;
using WebApiRest.NetCore.Models.Dtos;
using WebApiRest.NetCore.Models.Entity.SQLServer;

namespace WebApiRest.NetCore.Models.Tools
{
    public static class Extension
    {
        #region GroupMenu

        public static void ExtUpdate(this GroupMenu value, GroupMenuDto obj)
        {
            value.Title = obj.Title;
        }

        public static void ExtUpdate(this Entity.MySQL.TblGroupMenu value, GroupMenuDto obj)
        {
            value.Title = obj.Title;
        }

        #endregion GroupMenu

        #region Authorization

        public static void ExtUpdate(this Entity.SQLServer.Authorization value, AuthorizationDto obj)
        {
            value.Create = obj.Create;
            value.Read = obj.Read;
            value.Update = obj.Update;
            value.Delete = obj.Delete;
            value.CreationDate = obj.CreationDate;
        }

        public static void ExtUpdate(this Entity.MySQL.TblAuthorization value, AuthorizationDto obj)
        {
            value.Create = obj.Create;
            value.Read = obj.Read;
            value.Update = obj.Update;
            value.Delete = obj.Delete;
            value.CreationDate = obj.CreationDate;
        }

        #endregion Authorization

        #region User

        public static void ExtUpdate(this User value, UserDto obj)
        {
            value.IdRole = obj.IdRole;
            value.Login = obj.Login;
            value.Password = obj.Password;
            value.Name = obj.Name;
        }

        public static void ExtUpdate(this Entity.MySQL.TblUser value, UserDto obj)
        {
            value.IdRole = obj.IdRole;
            value.Login = obj.Login;
            value.Password = obj.Password;
            value.Name = obj.Name;
        }

        #endregion User

        #region Role

        public static void ExtUpdate(this Role value, RoleDto obj)
        {
            value.Title = obj.Title;
        }

        public static void ExtUpdate(this Entity.MySQL.TblRole value, RoleDto obj)
        {
            value.Title = obj.Title;
        }

        #endregion Role

        #region Menu

        public static void ExtUpdate(this Menu value, MenuDto obj)
        {
            value.Title = obj.Title;
            value.IdGroupMenu = obj.IdGroupMenu;
        }

        public static void ExtUpdate(this Entity.MySQL.TblMenu value, MenuDto obj)
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
