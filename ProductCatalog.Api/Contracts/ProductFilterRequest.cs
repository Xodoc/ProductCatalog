namespace ProductCatalog.Api.Contracts
{
    public class ProductFilterRequest
    {
        public string Name { get; set; } = string.Empty;

        public int CategoryId { get; set; }

        public string Description { get; set; } = string.Empty;

        public double CostFrom { get; set; }

        public double CostUpTo { get; set; }
    }
}
