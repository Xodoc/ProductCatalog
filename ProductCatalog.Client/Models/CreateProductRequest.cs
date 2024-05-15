namespace ProductCatalog.Client.Models
{
    public class CreateProductRequest
    {
        public string Name { get; set; }

        public int CategoryId { get; set; }

        public string Description { get; set; }

        public double CostInRubles { get; set; }

        public string GeneralNote { get; set; }

        public string SpecialNote { get; set; }
    }
}
