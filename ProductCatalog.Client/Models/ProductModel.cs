namespace ProductCatalog.Client.Models
{
    public class ProductModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }

        public double CostInRubles { get; set; }

        public string GeneralNote { get; set; }

        public string SpecialNote { get; set; }
    }
}
