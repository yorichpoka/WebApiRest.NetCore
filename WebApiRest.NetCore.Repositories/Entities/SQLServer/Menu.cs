using System.Collections.Generic;

namespace WebApiRest.NetCore.Repositories.Entities.SQLServer
{
    public partial class Menu
    {
        public Menu()
        {
            Authorization = new HashSet<Authorization>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int IdGroupMenu { get; set; }

        public virtual GroupMenu IdGroupMenuNavigation { get; set; }
        public virtual ICollection<Authorization> Authorization { get; set; }
    }
}