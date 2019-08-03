namespace WebApiRest.NetCore.Repositories.Entities.MySQL
{
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