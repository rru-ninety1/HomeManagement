namespace HomeManagement.Client.Features.Catalog.Products.Models
{
    public record class Product
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }
    }
}
