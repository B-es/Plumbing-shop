namespace Plumbing_shop.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Product>? Products { get; set; }
        public PageViewModel? PageViewModel { get; set; }
    }
}
