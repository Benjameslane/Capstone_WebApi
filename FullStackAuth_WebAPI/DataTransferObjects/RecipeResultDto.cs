namespace FullStackAuth_WebAPI.DataTransferObjects
{
    public class RecipeResultDto
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string ImageType { get; set; }
        public int Likes { get; set; }
        public int MissedIngredientCount { get; set; }
        public List<IngredientDto> MissedIngredients { get; set; }
        public string Title { get; set; }
        public List<IngredientDto> UnusedIngredients { get; set; }
        public int UsedIngredientCount { get; set; }
        public List<IngredientDto> UsedIngredients { get; set; }
    }
}