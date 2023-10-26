namespace FullStackAuth_WebAPI.DataTransferObjects
{
    public class PlaceResultDto
    {
        public string Name { get; set; }
        public string Vicinity { get; set; }
        public GeometryDto Geometry { get; set; }
    }
}
