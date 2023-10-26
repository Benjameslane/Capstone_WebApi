using Newtonsoft.Json;

namespace FullStackAuth_WebAPI.DataTransferObjects
{
    public class GooglePlacesResponseDto
    {
        [JsonProperty("results")]
        public IEnumerable<PlaceResultDto> Results { get; set; }
    }
}
