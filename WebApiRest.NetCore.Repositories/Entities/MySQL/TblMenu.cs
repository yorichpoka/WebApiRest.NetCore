using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiRest.NetCore.Repositories.Entities.MySQL
{
    public partial class TblMenu
    {
        public TblMenu()
        {
            this.C_Authorization = new HashSet<TblAuthorization>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int IdGroupMenu { get; set; }

        public virtual ICollection<TblAuthorization> C_Authorization { get; set; }
        public virtual TblGroupMenu GroupMenu { get; set; }
    }
}