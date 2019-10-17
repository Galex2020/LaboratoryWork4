using System;
using System.Text;
using System.Threading;
using static System.Console;

namespace LaboratoryWork4
{
    class Program
    {
        private static int x;
        private static int y;

        private static double ForceParse(string exMessage)
        {
            string inputString;
            double resultDouble;

            while (true)
            {
                if (double.TryParse(inputString = StringLimiter(8), out resultDouble))
                {
                    WriteLine();
                    return resultDouble;
                }

                StringCleaner(inputString);

                ForegroundColor = ConsoleColor.Red;
                Write("\a" + exMessage);
                Thread.Sleep(1250);
                ResetColor();

                StringCleaner(exMessage);
            }  
        }

        private static string StringLimiter(int limit)
        {
            var resultSB = new StringBuilder(limit);

            while (resultSB.Length < limit)
            {
                var keyPress = ReadKey();

                switch (keyPress.Key)
                {
                    case ConsoleKey.Enter:
                        return resultSB.ToString();

                    case ConsoleKey.Backspace:
                        if (resultSB.Length == 0)
                            break;
                        resultSB.Remove(resultSB.Length - 1, 1);
                        SetCursorPosition(x + resultSB.Length, y);
                        Write(' ');
                        SetCursorPosition(x + resultSB.Length, y);
                        break;

                    default:
                        resultSB.Append(keyPress.KeyChar);
                        break;
                }
            }

            return resultSB.ToString();
        }

        private static void StringCleaner(string stringToClear)
        {
            SetCursorPosition(x, y);
            Write(new string(' ', stringToClear.Length));
            SetCursorPosition(x, y);
        }

        static void Main(string[] args)
        {
            var rand = new Random();
            int rows = rand.Next(1, 8);
            int columns = rand.Next(1, 6);

            var inputArray = new double[rows, columns];
            var outputArray = new double[rows];

            ForegroundColor = ConsoleColor.Blue;
            WriteLine($"Введите элементы массива размерностью: {rows} x {columns}");
            ResetColor();

            for (var i = 0; i < rows; i++)
            {
                x = 0;
                y += 2;
                SetCursorPosition(0, y);

                for (var j = 0; j < columns; j++)
                {
                    inputArray[i, j] = ForceParse("Ошибка ввода!");

                    x += 10;
                    SetCursorPosition(x, y);

                    if (inputArray[i, j] < 0)
                        outputArray[i] += inputArray[i, j];
                }
            }

            SetCursorPosition(0, y + 2);

            ForegroundColor = ConsoleColor.Green;
            WriteLine("Новый массив:\n");

            foreach (var i in outputArray)
                WriteLine(i);

            ResetColor();
            ReadKey();
        }
    }
}
