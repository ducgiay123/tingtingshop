using ShopWeb.Core.src.Entity;

namespace EcomShop.Application.src.DTO
{
    public class CategoryCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }


    }
    public class CategoryReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

    }

    public class CategoryUpdateDto
    {
        // public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }


    }


}