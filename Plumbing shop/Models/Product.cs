using System.Xml.Linq;

namespace Plumbing_shop.Models
{
    public class Product
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

        public Product(string name, string type, string description)
        {
            Name = name;
            Type = type;
            Description = description;
        }
    }
}
