using System.Collections.Generic;

namespace WebApiRest.NetCore.Repositories.Entities.SqlServer
{
    public class GroupMenu
    {
        public GroupMenu()
        {
            Menus = new HashSet<Menu>();
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Menu> Menus { get; set; }
    }
}