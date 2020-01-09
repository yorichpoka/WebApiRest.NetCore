namespace WebApiRest.NetCore.Domain.Models.MongoDB
{
    public class RestaurantModel : ClassBaseModel
    {
        public AddressModel Address { get; set; }
        public string Borough { get; set; }
        public string Cuisine { get; set; }
        public string Restaurant_Id { get; set; }
        public string Name { get; set; }
        public GradeModel[] Grades { get; set; }
    }
}