using Plumbing_shop.Models;

namespace Plumbing_shop
{
    static public class RecModule
    {
        static private PlumbingDbContext Db;
        static public List<Models.Product> ExpSystem(string? data, PlumbingDbContext db)
        {
            Db = db;
            List<string> dataMas = data.Split(';').ToList();
            string? entityName = dataMas[0];
            dataMas.RemoveAt(0);
            List<string> values = dataMas;
            List<Entity> findEntities;
            List<Product> Products = new List<Product>();

            if (entityName == "ФЧ") findEntities = db.Entities.Where(entity => entity.Name != "Труба").ToList();
            else if (entityName == string.Empty) findEntities = db.Entities.ToList();
            else findEntities = db.Entities.Where(entity => entity.Name == entityName).ToList();

            var resEntities = selectionEntities(values, findEntities);

            foreach (var entity in resEntities)
            {
                Console.WriteLine($"Результат - {entity.Name}, id = {entity.Id}");
            }

            foreach (var entity in findEntities)
            {
                Products.Add(new Product(db, entity));
            }

            return Products;
        }

        static private List<Entity> selectionEntities(List<string> values, List<Entity> findEntities)
        {
            var ids = findEntities.Select(entity => entity.Id).ToList();
            foreach (string value in values)
            {
                for (int i = 0; i < ids.Count; i++)
                {
                    int count = Db.Values.Count(v => v.Id_Entity == ids[i] && v.value == value);
                    if (count == 0)
                    {
                        findEntities.RemoveAt(i);
                        ids.RemoveAt(i);
                    }
                }
            }
            return findEntities;
        }
    }
}
