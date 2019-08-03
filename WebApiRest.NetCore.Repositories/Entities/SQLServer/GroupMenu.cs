using System.Collections.Generic;

namespace WebApiRest.NetCore.Repositories.Entities.SQLServer
{
    public partial class GroupMenu
    {
        public GroupMenu()
        {
            Menu = new HashSet<Menu>();
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Menu> Menu { get; set; }
    }
}