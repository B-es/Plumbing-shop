using System.Collections.Generic;
using System.Linq;

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

    }
}