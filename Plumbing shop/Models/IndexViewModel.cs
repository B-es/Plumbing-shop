using System.Collections.Generic;

namespace Plumbing_shop.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Test> Products { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
