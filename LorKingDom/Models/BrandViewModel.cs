using System.Collections.Generic;        // để dùng List<T>

namespace LorKingDom.Models           // trùng với namespace của Brand
{
    public class BrandViewModel
    {
        public List<Brand> Brands      { get; set; } = new();
        public Brand       NewBrand    { get; set; } = new();
        public Brand       EditedBrand { get; set; } = new();
    }
}