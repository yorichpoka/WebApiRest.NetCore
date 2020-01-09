namespace WebApiRest.NetCore.Domain.Models
{
    public class GroupMenuModel : ClassBaseModel
    {
        public string Title { get; set; }
        public int[] IdMenus { get; set; }
    }
}