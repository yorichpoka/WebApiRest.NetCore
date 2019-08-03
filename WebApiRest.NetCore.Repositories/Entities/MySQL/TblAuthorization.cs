using System;

namespace WebApiRest.NetCore.Repositories.Entities.MySQL
{
    public class TblAuthorization
    {
        public int IdRole { get; set; }
        public int IdMenu { get; set; }
        public bool Create { get; set; }
        public bool Read { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual TblMenu Menu { get; set; }
        public virtual TblRole Role { get; set; }
    }
}