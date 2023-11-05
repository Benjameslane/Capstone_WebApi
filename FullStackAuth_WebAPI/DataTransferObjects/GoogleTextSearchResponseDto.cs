using Newtonsoft.Json;

namespace FullStackAuth_WebAPI.DataTransferObjects.GoogleTextSearch
{
    public class GoogleTextSearchResponseDto
    {
        [JsonProperty("results")]
        public List<TextSearchResultDto> Results { get; set; }
    }
}