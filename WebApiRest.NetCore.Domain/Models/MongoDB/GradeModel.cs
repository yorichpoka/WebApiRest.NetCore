using System;

namespace WebApiRest.NetCore.Domain.Models.MongoDB
{
    public class GradeModel : ClassBaseModel
    {
        public string Grade_Id { get; set; }
        public DateTime? Date { get; set; }
        public string Name { get; set; }
        public int? Score { get; set; }
    }
}