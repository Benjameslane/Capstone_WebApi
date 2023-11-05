using FullStackAuth_WebAPI.DataTransferObjects;
using Newtonsoft.Json;

namespace FullStackAuth_WebAPI.Services
{
    public class SpoonacularService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "4cb8d788dff14192aeeb7619916f5365";

        public SpoonacularService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        // ingredients = "apples,+flour,+sugar", number = # of recipes to return, ranking = (2) minimize missing ingredients
        public async Task<IEnumerable<UserRecipeDto>> GetRecipesByIngredientsAsync(string ingredients)
        {
            var url = $"recipes/findByIngredients?ingredients={ingredients}&number=30&ranking=2&apiKey={_apiKey}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<RecipeResultDto>>(content);

            return result.Select(r => new UserRecipeDto
            {
                Id = r.Id,
                Image = r.Image,
                Title = r.Title,
                MissedIngredients = r.MissedIngredients,
                UnusedIngredients = r.UnusedIngredients,
                UsedIngredients = r.UsedIngredients
            });
        }
    }
}
