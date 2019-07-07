using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiRest.NetCore.Models.Entity.MySQL
{
    [Table("user")]
    public class TblUser
    {
        public int Id { get; set; }
        public int IdRole { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }

        public virtual TblRole Role { get; set; }
    }
}