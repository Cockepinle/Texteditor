using System.Xml.Serialization;
using ConsoleApp29.Enums;
using Newtonsoft.Json;

namespace ConsoleApp29
{
    internal class FileWorker
    {
        /// <summary>
        /// Читаем все строки из файла
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string[] ReadFile(string path)
        {
            return File.ReadAllLines(path);
        }

        /// <summary>
        /// Сохранить файл 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="data"></param>
        /// <param name="type"></param>
        public void SaveFile(string filePath, object data, FileType type)
        {
            switch (type)
            {
                case FileType.Json:
                    SaveAsJsonFile(filePath, data);
                    break;
                case FileType.Xml:
                    SaveAsXmlFile(filePath, data); 
                    break;
                case FileType.Txt:
                    SaveAsTextFile(filePath, data); 
                    break;
            }

            ShowSaveInformation(filePath, type);
            Thread.Sleep(3000);
        }

        /// <summary>
        /// Сохранить как файл Json
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="data"></param>
        private void SaveAsJsonFile(string filePath, object data)
        {
            string json = JsonConvert.SerializeObject(data);
            File.WriteAllText(filePath, json);
        }

        /// <summary>
        /// Сохранить как файл xml
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="data"></param>
        private void SaveAsXmlFile(string filePath, object data)
        {
            XmlSerializer xml = new(typeof(List<Animal>));
            using StreamWriter sw = new(filePath);
            xml.Serialize(sw, data);
        }

        /// <summary>
        /// Сохранить как текстовый файл
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="data"></param>
        private void SaveAsTextFile(string filePath, object data)
        {
            var animalList = (List<Animal>)data;

            using StreamWriter sw = new(filePath);

            foreach (var animal in animalList)
            {
                sw.WriteLine(animal.Name);
                sw.WriteLine(animal.Age);
                sw.WriteLine(animal.Color);
            }
        }

        /// <summary>
        /// Показывать информацию о сохранении файла
        /// </summary>
        /// <param name="path"></param>
        /// <param name="type"></param>
        private void ShowSaveInformation(string path, FileType type)
        {
            Console.Clear();
            Console.WriteLine($"Файл успешно сохранен по пути: {path} в формате: {type}");
        }
    }
}
