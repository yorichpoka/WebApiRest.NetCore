using Package = WebApiRest.NetCore.Repositories.Entities.MySql;

namespace WebApiRest.NetCore.Repositories.Entities.MySql
{
    public class User
    {
        public int Id { get; set; }
        public int IdRole { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }

        public virtual Package.Role Role { get; set; }

        public User()
        {
        }
    }
}