namespace WebApiRest.NetCore.Domain.Models
{
    public class RoleModel : ClassBaseModel
    {
        public string Title { get; set; }
        public int[] IdUsers { get; set; }
    }
}