namespace WebApiRest.NetCore.Domain.Models
{
    public class UserModel : Model
    {
        public int IdRole { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
    }
}