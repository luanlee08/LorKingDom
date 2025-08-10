namespace LorKingDom.ViewModels
{
    public class ProductListItemViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = null!;
        public string? CategoryName { get; set; }
        public string? BrandName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? MainImage { get; set; }

        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }
        public int? PriceRangeId { get; set; }
        public int? SexId { get; set; }
        public int? AgeId { get; set; }
        public int? MaterialId { get; set; }
        public int? OriginId { get; set; }
        public string? Description { get; set; }
    }
}
