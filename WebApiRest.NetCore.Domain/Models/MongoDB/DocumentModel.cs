using System;

namespace WebApiRest.NetCore.Domain.Models.MongoDB
{
    public class DocumentModel : ClassBaseModel
    {
        public string Document_Id { get; set; }
        public string FullName { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public long Size { get; set; }
        public DateTime Date { get; set; }
        public byte[] Content { get; set; }
    }
}