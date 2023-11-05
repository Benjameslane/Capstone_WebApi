using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FullStackAuth_WebAPI.Models;
using FullStackAuth_WebAPI.DataTransferObjects.GooglePlaces;
using FullStackAuth_WebAPI.DataTransferObjects.GoogleTextSearch;

namespace FullStackAuth_WebAPI.Services
{
    public class GooglePlacesService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "AIzaSyA5kMbLRXGf110-1JzP35CkFlcFe69lnJ0";

        public GooglePlacesService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<IEnumerable<FoodShelter>> GetNearbyFoodSheltersAsync(double latitude, double longitude)
        {
            var url = $"nearbysearch/json?location={latitude},{longitude}&radius=50000&keyword=food%20shelter&key={_apiKey}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<GooglePlacesResponseDto>(content);

            return result.Results.Select(r => new FoodShelter
            {
                Name = r.Name,
                Vicinity = r.Vicinity,
                Latitude = r.Geometry.Location.Lat,
                Longitude = r.Geometry.Location.Lng
            });
        }

        public async Task<UserLocation> GetUserLocationAsync(string address)
        {
            var url = $"textsearch/json?query={address}&key={_apiKey}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<GoogleTextSearchResponseDto>(content);

            return new UserLocation
            {
                Latitude = result.Results[0].Geometry.Location.Lat,
                Longitude = result.Results[0].Geometry.Location.Lng
            };
        }
    }
}
