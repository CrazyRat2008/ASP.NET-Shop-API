using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Data.Entities
{
    public class CategoryEntity
    {
        [Key]
        public int Id { get; set; }
        public bool IsDelete { get; set; }
        public DateTime DateCreated { get; set; }
        [Required, StringLength(255)]
        public string Name { get; set; }
        public int Priority { get; set; }
        [StringLength(255)]
        public string Image { get; set; }
        [StringLength(4000)]
        public string Description { get; set; }
    }
}
