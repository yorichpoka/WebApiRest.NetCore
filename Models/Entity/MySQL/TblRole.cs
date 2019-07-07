using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiRest.NetCore.Models.Entity.MySQL
{
    [Table("role")]
    public class TblRole
    {
        public TblRole()
        {
            this.C_Authorization = new HashSet<TblAuthorization>();
            this.Users = new HashSet<TblUser>();
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<TblAuthorization> C_Authorization { get; set; }
        public virtual ICollection<TblUser> Users { get; set; }
    }
}