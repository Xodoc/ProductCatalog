namespace ProductCatalog.Api.Contracts
{
    public class UpdateProductRequest
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int CategoryId { get; set; }

        public string Description { get; set; } = string.Empty;

        public double CostInRubles { get; set; }

        public string GeneralNote { get; set; } = string.Empty;

        public string SpecialNote { get; set; } = string.Empty;
    }
}
