using LorKingDom.Models;

namespace LorKingDom.ViewModels
{
    public class AgeViewModel
    {
        public int AgeId { get; set; }

        public string AgeRange { get; set; } = null!;

        public bool IsDeleted { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    }
}
