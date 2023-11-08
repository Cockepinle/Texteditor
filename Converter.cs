using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace ConsoleApp29
{
    internal static class Converter
    {
        /// <summary>
        /// Метод который конвертирует строки из полученного файла в класс Animal
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static List<Animal> ConvertToAnimalObject(string[] data)
        {
            var animals = new List<Animal>();
            var animalsCount = data.Length / 3; // Мы делим на три, потому что у Animal - 3 свойства (Имя, возраст, цвет)

            for (int animalCount = 0, nameIndex = 0, ageIndex = 1, colorIndex = 2;  animalsCount > animalCount; animalCount++)
            {
                animals.Add(new Animal()
                {
                    Name = data[nameIndex],
                    Age = data[ageIndex],
                    Color = data[colorIndex],
                });

                nameIndex += 3;
                ageIndex += 3;
                colorIndex += 3;
            }

            return animals;
        }

        /// <summary>
        /// Метод который конвертирует json строку в список состоящий из классов Animal
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static List<Animal> ConvertToAnimalObjectFromJson(string json)
        {
            var animals = JsonConvert.DeserializeObject<List<Animal>>(json);
            return animals.ToList();
        }
        
        /// <summary>
        /// Метод который конвертирует xml в список состоящий из классов Animal
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static List<Animal> ConvertToAnimalObjectFromXml(string xml)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Animal>));

            using TextReader sr = new StringReader(xml);
            return (List<Animal>)xmlSerializer.Deserialize(sr);
        }
    }
}
