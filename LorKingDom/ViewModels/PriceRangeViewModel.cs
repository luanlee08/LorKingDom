using LorKingDom.Models;

namespace LorKingDom.ViewModels
{
    public class PriceRangeViewModel
    {
        public int PriceRangeId { get; set; }
        public string PriceRange { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
