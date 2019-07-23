using System;
using System.Collections.Generic;

namespace WebApiRest.NetCore.Repositories.Entities.SQLServer
{
    public partial class Role
    {
        public Role()
        {
            Authorization = new HashSet<Authorization>();
            User = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Authorization> Authorization { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
}
