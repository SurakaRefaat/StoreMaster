
namespace Store.Service.Services.ProductService.Dtos
{
    public class ProuductDetailsDto
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string pictureUrl { get; set; }
        public string Brandname { get; set; }
        public string TypeName { get; set; }

        public DateTime CreateAt { get; set; }

    }
}
