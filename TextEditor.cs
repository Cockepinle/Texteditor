using System.Security;

namespace ConsoleApp29
{
    internal class TextEditor
    {
        private int _xPosition;
        private int _yPosition;

        public Dictionary<int, string> Rows { get; set; }

        public TextEditor(int xPosition, int yPosition, Dictionary<int, string> rows)
        {
            _xPosition = xPosition;
            _yPosition = yPosition;

            Rows = rows;
        }

        public int Select()
        {
            ConsoleKeyInfo keyInfo;

            do
            {
                keyInfo = Console.ReadKey();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (Rows.Keys.First() != _yPosition)
                        {
                            --_yPosition;
                            _xPosition = Rows[_yPosition].Length;
                            Console.SetCursorPosition(_xPosition, _yPosition);
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (Rows.Keys.Last() != _yPosition)
                        {
                            ++_yPosition;
                            _xPosition = Rows[_yPosition].Length;
                            Console.SetCursorPosition(_xPosition, _yPosition);
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (0 < _xPosition)
                        {
                            --_xPosition;
                            Console.SetCursorPosition(_xPosition, _yPosition);
                        }
                        else
                        {
                            if (Rows.Keys.First() != _yPosition)
                            {
                                --_yPosition;
                                _xPosition = Rows[_yPosition].Length;
                                Console.SetCursorPosition(_xPosition, _yPosition);
                            }
                        }
                        break;
                    case ConsoleKey.RightArrow: 
                        if (Rows[_yPosition].Length > _xPosition)
                        {
                            ++_xPosition;
                            Console.SetCursorPosition(_xPosition, _yPosition);
                        }
                        else
                        {
                            if (Rows.Keys.Last() != _yPosition)
                            {
                                ++_yPosition;
                                _xPosition = Rows[_yPosition].Length;
                                Console.SetCursorPosition(_xPosition, _yPosition);
                            }
                        }
                        break;
                    case ConsoleKey.Backspace:
                        if (0 < _xPosition)
                        {
                            --_xPosition;
                            Console.Write(" \b");
                            var currentRow = Rows[_yPosition];
                            // Метод Remove убирает все символы после текущей позиции которую мы передали
                            Rows[_yPosition] = currentRow.Remove(_xPosition, 1);

                            ClearConsoleAndShowTip();
                            foreach (var row in Rows)
                                Console.WriteLine(row.Value);
                            
                            Console.SetCursorPosition(_xPosition, _yPosition);
                        }
                        break;
                }

                if (Char.IsLetterOrDigit(keyInfo.KeyChar))
                {
                    var currentRow = Rows[_yPosition];
                    var rightPartString = currentRow.Substring(_xPosition, currentRow.Length - _xPosition);
                    var newStringRow = currentRow.Replace(rightPartString, string.Empty);
                  
                    Rows[_yPosition] = string.Concat(newStringRow, keyInfo.KeyChar, rightPartString);
                    ++_xPosition;

                    ClearConsoleAndShowTip();
                    foreach (var row in Rows)
                        Console.WriteLine(row.Value);

                    Console.SetCursorPosition(_xPosition, _yPosition);
                }

            } while (keyInfo.Key != ConsoleKey.Escape && keyInfo.Key != ConsoleKey.F1);

            if (keyInfo.Key == ConsoleKey.Escape)
                return -1;
            else
                return 0;
        }

        private void ClearConsoleAndShowTip()
        {
            Console.Clear();
            Console.WriteLine("Сохраните файл в одном из трех форматов (txt, json, xml) - F1. Закрыть программу - Escape");
            Console.WriteLine("-----------------------------------------------------------------------------------------");
        }
    }
}
