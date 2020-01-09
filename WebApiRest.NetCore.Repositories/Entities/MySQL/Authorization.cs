using System;
using Package = WebApiRest.NetCore.Repositories.Entities.MySql;

namespace WebApiRest.NetCore.Repositories.Entities.MySql
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

        public virtual Package.Menu Menu { get; set; }
        public virtual Package.Role Role { get; set; }

        public Authorization()
        {
        }
    }
}