using System;
using WebApiRest.NetCore.Domain.Models.Struct;

namespace WebApiRest.NetCore.Domain.Models.SqLite
{
    public class WebSiteModel
    {
        public int Key { get; set; }
        public string Title { get; set; }
        public string Id { get; set; }
        public string Tags { get; set; }
        public string Url { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Group { get; set; }
        public string Place { get; set; }
        public CoordinateStruct? Location { get; set; }
        public string State { get; set; }
        public string License { get; set; }
        public string Author_Url { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? LastModified { get; set; }
        public DateTime? MetaDataModified { get; set; }
    }
}