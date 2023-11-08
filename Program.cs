namespace ConsoleApp29
{

    class Program
    {
        static void Main()
        {
            var fileWorker = new FileWorker(); 

            while (true)
            {
                ShowMenu();
                var filePath = Console.ReadLine(); // считаем имя файла
                var fileExtension = Path.GetExtension(filePath); // получаем расширение

                var lines = fileWorker.ReadFile(filePath); // считываем файл и получаем коллекцию его строк

                var rows = new Dictionary<int, string>();
                var defaultXPosition = 0;
                var defaultYPosition = 0;

                var additionalRows = ShowTip(); // получаем подсказку для пользователя

                // Данная итерация добавляет в коллекцию как ключ - номер строки, как значение - саму строку
                // Если итерация попадает на последнюю строку, то мы ставим позицию курсора на последний символ последней строки
                for (int x = 0; x < lines.Length; x++)
                {
                    rows.Add(x + additionalRows, lines[x]);
                    if (lines[x] == lines[lines.Length - 1])
                    {
                        defaultXPosition = lines[x].Length;
                        defaultYPosition = x + additionalRows;
                    }
                    Console.WriteLine(lines[x]);
                }

                var textEditor = new TextEditor(defaultXPosition, defaultYPosition, rows);
                var selectResult = textEditor.Select(); // ждем ввода пользователя (символа)

                if (selectResult == -1) // Выходим из программы
                    return;
                else // Начинаем процесс сохранения файла в другой или тот же формат
                {
                    ShowSaveCommandText();
                    var newFile = Console.ReadLine(); // получаем ввод пользователя (путь до нового файла, который потребуется создать)
                    var ext = Path.GetExtension(newFile)?.ToLower(); // получаем расширение данного файла

                    var textValues = textEditor.Rows.Values.ToArray(); // получаем коллекцию всех строк входного файла
                    var textValue = string.Join(string.Empty, textValues); // тут мы получаем все строки в виде одной строки

                    var resultValue = new List<Animal>();
                    switch (fileExtension.ToLower()) // Переводим в нижний регистр
                    {
                        case ".json":
                            resultValue = Converter.ConvertToAnimalObjectFromJson(textValue); // Конвертируем json строку в объект Animal
                            break;
                        case ".xml":
                            resultValue = Converter.ConvertToAnimalObjectFromXml(textValue); // Конвертируем xml строку в объект Animal
                            break;
                        case ".txt":
                            resultValue = Converter.ConvertToAnimalObject(textValues); // Конвертируем строки в объект Animal
                            break;
                    }
                  

                    switch (ext)
                    {
                        case ".json":
                            fileWorker.SaveFile(newFile, resultValue, Enums.FileType.Json); // Сохраняем файл в формате json
                            break;
                        case ".xml":
                            fileWorker.SaveFile(newFile, resultValue, Enums.FileType.Xml); // Сохраняем файл в формате xml
                            break;
                        case ".txt":
                            fileWorker.SaveFile(newFile, resultValue, Enums.FileType.Txt); // Сохраняем файл в формате txt
                            break;
                    }
                }
            }
        }
        
        /// <summary>
        /// Показываем первоначальное меню с выбором файла
        /// </summary>
        static void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("Введите путь до файла (вместе с названием), который вы хотите открыть:");
            Console.WriteLine("----------------------------------------------------------------------");
        }

        /// <summary>
        /// Показываем подсказку по работе с файлом
        /// </summary>
        /// <returns></returns>
        static int ShowTip()
        {
            Console.Clear();
            var consoleRowsCount = 0;
            Console.WriteLine("Сохраните файл в одном из трех форматов (txt, json, xml) - F1. Закрыть программу - Escape");
            ++consoleRowsCount;
            Console.WriteLine("-----------------------------------------------------------------------------------------");
            ++consoleRowsCount;
            return consoleRowsCount;
        }

        /// <summary>
        /// Показываем подсказку с командой - "Сохранить файл" и куда его сохранить
        /// </summary>
        static void ShowSaveCommandText()
        {
            Console.Clear();
            Console.WriteLine("Введите путь до файла (вместе с названием), куда вы хотите сохранить текст:");
            Console.WriteLine("---------------------------------------------------------------------------");
        }    
    }

}