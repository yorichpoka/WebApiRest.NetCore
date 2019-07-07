namespace WebApiRest.NetCore.Models.Dtos
{
    public class UserDto : Dto
    {
        public int IdRole { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
    }
}