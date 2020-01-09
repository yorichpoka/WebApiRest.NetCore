using System.Collections.Generic;

namespace WebApiRest.NetCore.Repositories.Entities.SqlServer
{
    public class Menu
    {
        public Menu()
        {
            Authorizations = new HashSet<Authorization>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int IdGroupMenu { get; set; }

        public virtual GroupMenu GroupMenu { get; set; }
        public virtual ICollection<Authorization> Authorizations { get; set; }
    }
}