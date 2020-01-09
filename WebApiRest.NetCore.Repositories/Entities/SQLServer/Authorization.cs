using System;

namespace WebApiRest.NetCore.Repositories.Entities.SqlServer
{
    public class Authorization
    {
        public int IdRole { get; set; }
        public int IdMenu { get; set; }
        public bool Create { get; set; }
        public bool Read { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual Menu Menu { get; set; }
        public virtual Role Role { get; set; }
    }
}