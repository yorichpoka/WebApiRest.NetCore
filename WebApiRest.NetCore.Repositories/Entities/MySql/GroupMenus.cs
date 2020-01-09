using System.Collections.Generic;
using Package = WebApiRest.NetCore.Repositories.Entities.MySql;

namespace WebApiRest.NetCore.Repositories.Entities.MySql
{
    public class GroupMenu
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Package.Menu> Menus { get; set; }

        public GroupMenu()
        {
            this.Menus = new HashSet<Package.Menu>();
        }
    }
}