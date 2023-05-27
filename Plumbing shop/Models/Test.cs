using System.Xml.Linq;

namespace Plumbing_shop.Models
{
    public class Test
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

        public Test(string name, string type, string description)
        {
            Name = name;
            Type = type;
            Description = description;
        }
    }
}
