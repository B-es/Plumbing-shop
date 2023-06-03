using Microsoft.CodeAnalysis.VisualBasic.Syntax;
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

            List<Entity> findOtherEnt;

            List<Product> Products = new List<Product>();

            if (entityName == "ФЧ") findEntities = db.Entities.Where(entity => entity.Name != "Труба").ToList();
            else if (entityName == string.Empty) findEntities = db.Entities.ToList();
            else findEntities = db.Entities.Where(entity => entity.Name == entityName).ToList();

            findOtherEnt = db.Entities.Where(entity => entity.Name != entityName).ToList();

            List<Value>? test = db.Values.Where(v => v.Id_Attribute == 3 && v.Id_Entity == 3).ToList();
            List<string?> test2 = new List<string?>();
            foreach (var val in test)
            {
                test2.Add(val.value);
            }
            Console.WriteLine(test2[0]);

            var resEntities = selectionEntities(values, findEntities);

            var recEntities = getRecProducts(values, findOtherEnt);

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

        static private List<Entity> getRecProducts(List<string> values, List<Entity> findOtherEnt)
        {
            var ids = findOtherEnt.Select(entity => entity.Id).ToList();
            bool сompatibility;
            foreach (string value in values)
            {
                for (int i = 0; i<ids.Count; i++)
                {
                    сompatibility = rools(Db, value, i);
                    if (сompatibility == false)
                    {
                        findOtherEnt.RemoveAt(i);
                        ids.RemoveAt(i);
                    }
                    
                }
            }

            return findOtherEnt;
        }

        static private bool rools(PlumbingDbContext db, string value, int currentId)
        {
            bool materialFlag = false;
            bool tempFlag = false;
            bool connectFlag = false;
            bool diamFlag = false;

            // Проверка совместимости по температурному режиму (Трубы) - материалу (Фасонные части)
            if (value == "От 5 до 30 градусов" || value == "От 30 до 65 градусов" || value == "От 65 до 80 градусов" || value == "От 80 до 120 градусов")
            {
                // Достаем значение из БД
                List<Value>? test = db.Values.Where(v => v.Id_Attribute == 3 && v.Id_Entity == currentId).ToList();
                List<string?> material1 = new List<string?>();
                foreach (var val in test)
                {
                    material1.Add(val.value);
                }

                string material = material1[0];

                if (value == "От 5 до 30 градусов" && (material == "Сталь" || material == "Медь" || material == "Полиэтилен" || material == "ПВХ" || material == "Чугун" || material == "Бетон"))
                {
                    materialFlag = true;
                }
                else
                {
                    materialFlag = false;
                }

                if (value == "От 30 до 65 градусов" && (material == "Сталь" || material == "Медь" || material == "Полиэтилен" || material == "ПВХ" || material == "Чугун"))
                {
                    materialFlag = true;
                }
                else
                {
                    materialFlag = false;
                }

                if (value == "От 65 до 80 градусов" && (material == "Сталь" || material == "Медь" || material == "Полиэтилен" || material == "Чугун"))
                {
                    materialFlag = true;
                }
                else
                {
                    materialFlag = false;
                }

                if (value == "От 80 до 120 градусов" && (material == "Сталь" || material == "Медь" || material == "Чугун"))
                {
                    materialFlag = true;
                }
                else
                {
                    materialFlag = false;
                }
            }

            // Проверка совместимости по материалу (Фасонные части) - температурному режиму (Трубы)
            if (value == "Медь" || value == "Сталь" || value == "Чугун" || value == "ПВХ" || value == "Полиэтилен")
            {
                // Достаём значение из БД
                List<Value>? test = db.Values.Where(v => v.Id_Attribute == 2 && v.Id_Entity == currentId).ToList();
                List<string?> temperatureMas = new List<string?>();
                foreach (var val in test)
                {
                    temperatureMas.Add(val.value);
                }

                string temperature = temperatureMas[0];
                
                if (value == "Медь" && (temperature == "От 5 до 30 градусов" || temperature == "От 30 до 65 градусов" || temperature == "От 65 до 80 градусов" || temperature == "От 80 до 120 градусов"))
                {
                    materialFlag = true;
                }
                else
                {
                    materialFlag = false;
                }

                if (value == "Сталь" && (temperature == "От 5 до 30 градусов" || temperature == "От 30 до 65 градусов" || temperature == "От 65 до 80 градусов" || temperature == "От 80 до 120 градусов"))
                {
                    materialFlag = true;
                }
                else
                {
                    materialFlag = false;
                }

                if (value == "Чугун" && (temperature == "От 5 до 30 градусов" || temperature == "От 30 до 65 градусов" || temperature == "От 65 до 80 градусов" || temperature == "От 80 до 120 градусов"))
                {
                    materialFlag = true;
                }
                else
                {
                    materialFlag = false;
                }

                if (value == "ПВХ" && (temperature == "От 5 до 30 градусов" || temperature == "От 30 до 65 градусов"))
                {
                    materialFlag = true;
                }
                else
                {
                    materialFlag = false;
                }

                if (value == "Полиэтилен" && (temperature == "От 5 до 30 градусов" || temperature == "От 30 до 65 градусов"))
                {
                    materialFlag = true;
                }
                else
                {
                    materialFlag = false;
                }
            }

            // Проверка совместимости Типа соединения
            if (value == "Разъёмный" || value == "Неразъёмный")
            {
                // Достаём значение из БД
                List<Value>? test = db.Values.Where(v => v.Id_Attribute == 4 && v.Id_Entity == currentId).ToList();
                List<string?> connectionMas = new List<string?>();
                foreach (var val in test)
                {
                    connectionMas.Add(val.value);
                }
                string connection = connectionMas[0];

                if (value == connection)
                {
                    connectFlag = true;
                }
                else
                {
                    connectFlag = false;
                }
            }

            // Проверка совместимости по Диаметру
            if (value == "15" || value == "20" || value == "50" || value == "80")
            {
                // Достаём значение из БД
                List<Value>? test = db.Values.Where(v => v.Id_Attribute == 5 && v.Id_Entity == currentId).ToList();
                List<string?> diameterMas = new List<string?>();
                foreach (var val in test)
                {
                    diameterMas.Add(val.value);
                }
                string diameter = diameterMas[0];

                if (value == diameter)
                {
                    diamFlag = true;
                }
                else
                {
                    diamFlag = false;
                }
            }

            // Определяем подходит ли товар с учётом рассмотренных критериев совместимости
            if (materialFlag == true && connectFlag == true && tempFlag == true && diamFlag == true)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
