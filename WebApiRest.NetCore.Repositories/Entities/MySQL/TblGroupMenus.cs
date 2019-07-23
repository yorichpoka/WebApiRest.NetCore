using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiRest.NetCore.Repositories.Entities.MySQL
{
    public class TblGroupMenu
    {
        public TblGroupMenu()
        {
            this.Menus = new HashSet<TblMenu>();
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<TblMenu> Menus { get; set; }
    }
}