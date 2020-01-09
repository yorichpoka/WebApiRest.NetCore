namespace WebApiRest.NetCore.Domain.Models
{
    public class MenuModel : ClassBaseModel
    {
        public string Title { get; set; }
        public int? IdGroupMenu { get; set; }
    }
}