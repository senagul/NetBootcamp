namespace Bootcamp.Repository.Products
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; } = default!;
        public decimal Price { get; set; }

        public DateTime Created { get; set; } = new();
        public int Stock { get; set; }

        public string Barcode { get; init; } = default!;
    }
}