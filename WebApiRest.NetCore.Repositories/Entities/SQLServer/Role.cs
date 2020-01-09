using System.Collections.Generic;

namespace WebApiRest.NetCore.Repositories.Entities.SqlServer
{
    public class Role
    {
        public Role()
        {
            Authorizations = new HashSet<Authorization>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Authorization> Authorizations { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}