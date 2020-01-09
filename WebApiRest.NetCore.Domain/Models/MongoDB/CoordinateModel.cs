using WebApiRest.NetCore.Domain.Models.Struct;

namespace WebApiRest.NetCore.Domain.Models.MongoDB
{
    public class CoordinateModel : ClassBaseModel
    {
        public string Coordinate_Id { get; set; }
        public string Type { get; set; }
        public CoordinateStruct? Coordinate { get; set; }
    }
}