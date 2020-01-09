namespace WebApiRest.NetCore.Domain.Models.MongoDB
{
    public class AddressModel : ClassBaseModel
    {
        public string Address_Id { get; set; }
        public string Building { get; set; }
        public CoordinateModel Coordinate { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
    }
}