namespace WebApiRest.NetCore.Repositories.Entities.SQLServer
{
    public partial class User
    {
        public int Id { get; set; }
        public int IdRole { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }

        public virtual Role Role { get; set; }
    }
}