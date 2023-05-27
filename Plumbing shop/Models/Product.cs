namespace Plumbing_shop.Models
{
    public class Product
    {
        public Entity entity;
        public List<Attribute> attributes = new List<Attribute>();
        public List<Value> values;

        public Product(PlumbingDbContext db, int? id)
        {
            //Сущность
            entity = db.Entities.Find(id);

            //Аттрибуты
            List<Value> values_att = db.Values.Where(s => s.Id_Entity == id).ToList();
            List<int> att_ids = new List<int>();
            foreach (var att in values_att) att_ids.Add(att.Id_Attribute);
            for (int i = 0; i < att_ids.Count; i++) attributes.Add(db.Attributes.Find(att_ids[i]));

            //Значения
            values = db.Values.Where(s => s.Id_Entity == id).ToList();
        }

        static public List<Product> createProducts(PlumbingDbContext db) 
        { 
            var products = new List<Product>();
            int count = db.Entities.Count();
            for(int i = 0; i < count; i++)
            {
                products.Add(new Product(db, i+1));
            }
            return products;
        }
    }
}