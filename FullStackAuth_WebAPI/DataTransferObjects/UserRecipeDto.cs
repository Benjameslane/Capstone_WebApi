namespace FullStackAuth_WebAPI.DataTransferObjects
{
    public class UserRecipeDto
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public List<IngredientDto> MissedIngredients { get; set; }
        public List<IngredientDto> UnusedIngredients { get; set; }
        public List<IngredientDto> UsedIngredients { get; set; }
    }
}