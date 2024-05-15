namespace ProductCatalog.Api.Contracts
{
    public class ProductResponse
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string CategoryName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public double CostInRubles { get; set; }

        public string GeneralNote { get; set; } = string.Empty;

        public string SpecialNote { get; set; } = string.Empty;
    }
}
