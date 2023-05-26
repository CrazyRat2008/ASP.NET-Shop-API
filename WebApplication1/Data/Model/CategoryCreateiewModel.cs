namespace WebApplication1.Data.Model
{
    public class CategoryCreateiewModel
    {
        public string Name { get; set; }
        public int Priority { get; set; } 
        public string Description { get; set; }
        public IFormFile Image { get; set; }
    }
}
