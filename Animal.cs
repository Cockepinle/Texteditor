using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp29
{
    /// <summary>
    ///  Класс, который описывает животное
    /// </summary>
    [Serializable] // Требуется для сериализации в различные форматы - иначе ошибка (например xml)
    public class Animal
    {
        public string Name { get; set; }
        public string Age { get; set; }
        public string Color { get; set; }

        public Animal()
        {
            
        }
        public Animal(string name, string age, string color)
        {
            Name = name;
            Age = age;
            Color = color;
        }

        public override string ToString()
        {
            return new string($"{Name} {Age} {Color}");
        }
    }
}
