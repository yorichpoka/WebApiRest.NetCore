namespace WebApiRest.NetCore.Domain.Models
{
    public class UserRoleModel : ClassBaseModel
    {
        public int IdRole { get; set; }
        public RoleModel Role { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
    }
}