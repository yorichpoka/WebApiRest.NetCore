using System.Collections.Generic;
using Package = WebApiRest.NetCore.Repositories.Entities.MySql;

namespace WebApiRest.NetCore.Repositories.Entities.MySql
{
    public class Role
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Package.Authorization> Authorizations { get; set; }
        public virtual ICollection<Package.User> Users { get; set; }

        public Role()
        {
            this.Authorizations = new HashSet<Package.Authorization>();
            this.Users = new HashSet<Package.User>();
        }
    }
}