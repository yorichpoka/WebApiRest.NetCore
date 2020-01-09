using System.Linq;
using WebApiRest.NetCore.Domain;
using WebApiRest.NetCore.Domain.Models;
using WebApiRest.NetCore.Domain.Models.MongoDB;
using WebApiRest.NetCore.Domain.Models.SqLite;
using WebApiRest.NetCore.Repositories.Entities.MongoDB;
using WebApiRest.NetCore.Repositories.Entities.SqLite;
using MySqlPkg = WebApiRest.NetCore.Repositories.Entities.MySql;
using SqlServerPkg = WebApiRest.NetCore.Repositories.Entities.SqlServer;

namespace WebApiRest.NetCore.Repositories
{
    public static class Extension
    {
        #region GroupMenu

        public static void ExtUpdate(this SqlServerPkg.GroupMenu value, GroupMenuModel obj)
        {
            value.Title = obj.Title;
        }

        public static void ExtUpdate(this MySqlPkg.GroupMenu value, GroupMenuModel obj)
        {
            value.Title = obj.Title;
        }

        #endregion GroupMenu

        #region Authorization

        public static void ExtUpdate(this SqlServerPkg.Authorization value, AuthorizationModel obj)
        {
            if (obj.IdRole.HasValue)
                value.IdRole = obj.IdRole.Value;
            if (obj.IdMenu.HasValue)
                value.IdMenu = obj.IdMenu.Value;
            if (obj.Create.HasValue)
                value.Create = obj.Create.Value;
            if (obj.Read.HasValue)
                value.Read = obj.Read.Value;
            if (obj.Update.HasValue)
                value.Update = obj.Update.Value;
            if (obj.Delete.HasValue)
                value.Delete = obj.Delete.Value;
        }

        public static void ExtUpdate(this MySqlPkg.Authorization value, AuthorizationModel obj)
        {
            if (obj.Create.HasValue)
                value.Create = obj.Create.Value;
            if (obj.Read.HasValue)
                value.Read = obj.Read.Value;
            if (obj.Update.HasValue)
                value.Update = obj.Update.Value;
            if (obj.Delete.HasValue)
                value.Delete = obj.Delete.Value;
        }

        #endregion Authorization

        #region User

        public static void ExtUpdate(this SqlServerPkg.User value, UserModel obj)
        {
            if (obj.IdRole.HasValue)
                value.IdRole = obj.IdRole.Value;
            if (obj.Login != null)
                value.Login = obj.Login;
            if (obj.Password != null)
                value.Password = obj.Password;
            if (obj.Name != null)
                value.Name = obj.Name;
        }

        public static void ExtUpdate(this MySqlPkg.User value, UserModel obj)
        {
            if (obj.IdRole.HasValue)
                value.IdRole = obj.IdRole.Value;
            if (obj.Login != null)
                value.Login = obj.Login;
            if (obj.Password != null)
                value.Password = obj.Password;
            if (obj.Name != null)
                value.Name = obj.Name;
        }

        #endregion User

        #region Role

        public static void ExtUpdate(this SqlServerPkg.Role value, RoleModel obj)
        {
            value.Title = obj.Title;
        }

        public static void ExtUpdate(this MySqlPkg.Role value, RoleModel obj)
        {
            value.Title = obj.Title;
        }

        #endregion Role

        #region WebSite

        public static void ExtUpdate(this WebSite value, WebSiteModel obj)
        {
            if (obj.Author != null)
                value.Author = obj.Author;
            if (obj.Author_Url != null)
                value.Author_Url = obj.Author_Url;
            if (obj.Description != null)
                value.Description = obj.Description;
            if (obj.Group != null)
                value.Group = obj.Group;
            if (obj.LastModified.HasValue)
                value.LastModified = obj.LastModified.Value.ToString("s");
            if (obj.License != null)
                value.License = obj.License;
            if (obj.Location.HasValue)
                value.Location = obj.Location.ExtConvertToString();
            if (obj.MetaDataModified.HasValue)
                value.MetaDataModified = obj.MetaDataModified.Value.ToString("s");
            if (obj.Url != null)
                value.Url = obj.Url;
            if (obj.Title != null)
                value.Title = obj.Title;
            if (obj.Tags != null)
                value.Tags = obj.Tags;
            if (obj.State != null)
                value.State = obj.State;
            if (obj.Place != null)
                value.Place = obj.Place;
        }

        #endregion WebSite

        #region Menu

        public static void ExtUpdate(this SqlServerPkg.Menu value, MenuModel obj)
        {
            if (obj.Title != null)
                value.Title = obj.Title;
            if (obj.IdGroupMenu.HasValue)
                value.IdGroupMenu = obj.IdGroupMenu.Value;
        }

        public static void ExtUpdate(this MySqlPkg.Menu value, MenuModel obj)
        {
            if (obj.Title != null)
                value.Title = obj.Title;
            if (obj.IdGroupMenu.HasValue)
                value.IdGroupMenu = obj.IdGroupMenu.Value;
        }

        #endregion Menu

        #region Restaurant

        public static void ExtUpdate(this Restaurant value, RestaurantModel obj)
        {
            if (obj.Name != null)
                value.Name = obj.Name;
            if (obj.Borough != null)
                value.Borough = obj.Borough;
            if (obj.Cuisine != null)
                value.Cuisine = obj.Cuisine;
            if (obj.Grades != null)
                value.Grades.ToList()
                            .ForEach(
                                (grade) =>
                                {
                                    var index = 0;
                                    grade.ExtUpdate(
                                        obj.Grades[(index++)]
                                    );
                                }
                            );
            if (obj.Address != null)
                value.Address.ExtUpdate(obj.Address);
        }

        #endregion Restaurant

        #region Grade

        public static void ExtUpdate(this Grade value, GradeModel obj)
        {
            if (obj.Name != null)
                value.Name = obj.Name;
            if (obj.Date.HasValue)
                value.Date = obj.Date;
            if (obj.Score.HasValue)
                value.Score = obj.Score;
        }

        #endregion Grade

        #region Document

        public static void ExtUpdate(this Document value, DocumentModel obj)
        {
            value.Date = obj.Date;
            value.Extension = obj.Extension;
            value.FullName = obj.FullName;
            value.Name = obj.Name;
            value.Size = obj.Size;
        }

        #endregion Document

        #region Coordinate

        public static void ExtUpdate(this Coordinate value, CoordinateModel obj)
        {
            if (obj.Coordinate.HasValue)
                value.Coordinates = obj.Coordinate.ExtConvertToArray().ToArray();
            if (obj.Type != null)
                value.Type = obj.Type;
        }

        #endregion Coordinate

        #region Address

        public static void ExtUpdate(this Address value, AddressModel obj)
        {
            if (obj.Building != null)
                value.Building = obj.Building;
            if (obj.Street != null)
                value.Street = obj.Street;
            if (obj.ZipCode != null)
                value.ZipCode = obj.ZipCode;
            if (obj.Coordinate != null)
                value.Coordinate.ExtUpdate(obj.Coordinate);
        }

        #endregion Address
    }
}