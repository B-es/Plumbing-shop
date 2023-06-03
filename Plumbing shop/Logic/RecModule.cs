using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Newtonsoft.Json.Linq;
using Plumbing_shop.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Plumbing_shop
{
    static public class RecModule
    {
        static private PlumbingDbContext Db;

        static private List<string> cutterList(List<string> strings)
        {
            List<string> values = new List<string>(strings);
            for (int i = 0; i < values.Count; i++)
                values[i] = values[i].Substring(values[i].IndexOf(":") + 1, values[i].Length - values[i].IndexOf(":") - 1);
            return values;
        }
        
        static public List<Product> selectProducts(string? data, PlumbingDbContext db)
        {
            Db = db;
            var (values, findEntities) = getDataForSelection(data);
            return selectionProducts(values, findEntities);
        }

        static private (List<string>, List<Entity>) getDataForSelection(string? data)
        {
            
            List<string> dataMas = data.Split(';').ToList();
            string? entityName = dataMas[0];
            dataMas.RemoveAt(0);
            List<string> values = cutterList(dataMas);
            List<Entity> findEntities;

            if (entityName == "ФЧ") findEntities = Db.Entities.Where(entity => entity.Name != "Труба").ToList();
            else if (entityName == string.Empty) findEntities = Db.Entities.ToList();
            else findEntities = Db.Entities.Where(entity => entity.Name == entityName).ToList();

            return (values, findEntities);
        }

        static private List<Product> selectionProducts(List<string> values, List<Entity> findEntities)
        {
            List<Product> Products = new List<Product>();
            var ids = findEntities.Select(entity => entity.Id).ToList();
            List<Entity> entities = new List<Entity>(findEntities);
            foreach (string value in values)
            {
                for (int i = 0; i < ids.Count; i++)
                {
                    int count = Db.Values.Count(v => v.Id_Entity == ids[i] && v.value == value);
                    if (count == 0)
                    {
                        entities.RemoveAt(i);
                        ids.RemoveAt(i);
                    }
                }
            }

            foreach (var entity in entities)
            {
                Products.Add(new Product(Db, entity));
            }

            return Products;
        }

        static public List<Product> selectRecProduct(string? data, PlumbingDbContext db)
        {
            Db = db;
            var (values, findOtherEnt) = getDataForRec(data);
            return getRecProducts(values, findOtherEnt);
        }

        static private (List<string>, List<Entity>) getDataForRec(string? data)
        {
            List<string> dataMas = data.Split(';').ToList();
            string? entityName = dataMas[0];
            dataMas.RemoveAt(0);

            List<Entity> findOtherEnt;
            findOtherEnt = Db.Entities.Where(entity => entity.Name != entityName).ToList();

            return (dataMas, findOtherEnt);
        }

        static private List<Product> getRecProducts(List<string> vals, List<Entity> findOtherEnt)
        {
            List<Product> Products = new List<Product>();
            var ids = findOtherEnt.Select(entity => entity.Id).ToList();
            List<Entity> entities = new List<Entity>(findOtherEnt);
            List<string> values = new List<string>(vals);
            bool сompatibility;
            bool isTruba = false;

            for(int i = 0; i < values.Count; i++)
                if (values[i].Contains("Тип трубопровода") || values[i].Contains("Длина")) isTruba = true;

            if(isTruba) 
                for (int i = 0; i < values.Count; i++)
                {
                    if (values[i].Contains("Производитель") || values[i].Contains("Длина") ||
                        values[i].Contains("Тип трубопровода") || values[i].Contains("Материал")) { values.RemoveAt(i); i = -1; } 
                }
            else
                for (int i = 0; i < values.Count; i++)
                {
                    if (values[i].Contains("Температурный режим")) { values.RemoveAt(i); i = -1; }
                }

            values = cutterList(values);
            
            for (int i = 0; i<ids.Count; ++i)
            {
                foreach (string value in values)
                {
                    сompatibility = rools(value, ids[i], isTruba);
                    if (!сompatibility)
                    {
                        entities.RemoveAt(i);
                        ids.RemoveAt(i);
                        i = -1;
                        break;
                    }
                }
            }

            foreach (var entity in entities)
            {
                Products.Add(new Product(Db, entity));
            }

            return Products;
        }

        static private bool rools(string value, int? currentId, bool isTruba)
        {
            bool materialFlag = false;
            bool tempFlag = false;
            bool connectFlag = false;
            bool diamFlag = false;

            // Проверка совместимости по температурному режиму (Трубы) - материалу (Фасонные части)
            if (isTruba && (value == "От 5 до 30 градусов" || value == "От 30 до 65 градусов" || value == "От 65 до 80 градусов" || value == "От 80 до 120 градусов"))
            {
                // Достаем значение из БД
                Value? test = Db.Values.Where(v => v.Id_Attribute == 3 && v.Id_Entity == currentId).SingleOrDefault();
                string? material = "";
                if (test != null)
                    material = test.value;

                if (value == "От 5 до 30 градусов" && (material == "Сталь" || material == "Медь" || material == "Полиэтилен" || material == "ПВХ" || material == "Чугун" || material == "Бетон"))
                {
                    materialFlag = true;
                }
                else if (value == "От 30 до 65 градусов" && (material == "Сталь" || material == "Медь" || material == "Полиэтилен" || material == "ПВХ" || material == "Чугун"))
                {
                    materialFlag = true;
                }
                else if (value == "От 65 до 80 градусов" && (material == "Сталь" || material == "Медь" || material == "Полиэтилен" || material == "Чугун"))
                {
                    materialFlag = true;
                }
                else if (value == "От 80 до 120 градусов" && (material == "Сталь" || material == "Медь" || material == "Чугун"))
                {
                    materialFlag = true;
                }
                else
                {
                    materialFlag = false;
                }
            }   

            // Проверка совместимости по материалу (Фасонные части) - температурному режиму (Трубы)
            if (!isTruba && (value == "Медь" || value == "Сталь" || value == "Чугун" || value == "ПВХ" || value == "Полиэтилен"))
            {
                // Достаём значение из БД
                Value? test = Db.Values.Where(v => v.Id_Attribute == 2 && v.Id_Entity == currentId).SingleOrDefault();
                string? temperature = "";
                if (test != null)
                    temperature = test.value;

                if (value == "Медь" && (temperature == "От 5 до 30 градусов" || temperature == "От 30 до 65 градусов" || temperature == "От 65 до 80 градусов" || temperature == "От 80 до 120 градусов"))
                {
                    materialFlag = true;
                }
                else if (value == "Сталь" && (temperature == "От 5 до 30 градусов" || temperature == "От 30 до 65 градусов" || temperature == "От 65 до 80 градусов" || temperature == "От 80 до 120 градусов"))
                {
                    materialFlag = true;
                }
                else if (value == "Чугун" && (temperature == "От 5 до 30 градусов" || temperature == "От 30 до 65 градусов" || temperature == "От 65 до 80 градусов" || temperature == "От 80 до 120 градусов"))
                {
                    materialFlag = true;
                }
                else if (value == "ПВХ" && (temperature == "От 5 до 30 градусов" || temperature == "От 30 до 65 градусов"))
                {
                    materialFlag = true;
                }
                else if (value == "Полиэтилен" && (temperature == "От 5 до 30 градусов" || temperature == "От 30 до 65 градусов"))
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
                Value? test = Db.Values.Where(v => v.Id_Attribute == 4 && v.Id_Entity == currentId).SingleOrDefault();
                string? connection = "";
                if (test != null)
                    connection = test.value;

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
            if (value == "15мм" || value == "20мм" || value == "50мм" || value == "80мм")
            {
                // Достаём значение из БД
                Value? test = Db.Values.Where(v => v.Id_Attribute == 5 && v.Id_Entity == currentId).SingleOrDefault();
                string? diameter = "";
                if (test != null)
                    diameter = test.value;

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
            return materialFlag || connectFlag || diamFlag || tempFlag;

        }
    }
}
