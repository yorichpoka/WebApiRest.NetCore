using System.Collections.Generic;
using Package = WebApiRest.NetCore.Repositories.Entities.MySql;

namespace WebApiRest.NetCore.Repositories.Entities.MySql
{
    public class Menu
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int IdGroupMenu { get; set; }

        public virtual ICollection<Package.Authorization> Authorizations { get; set; }
        public virtual Package.GroupMenu GroupMenu { get; set; }

        public Menu()
        {
            this.Authorizations = new HashSet<Package.Authorization>();
        }
    }
}