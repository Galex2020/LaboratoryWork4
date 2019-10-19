// Необходимо выполнить заданную обработку числового двумерного массива,
// имеющего произвольное количество строк (N) и столбцов (M). N <= 7, M <= 5.
// Элементы массива должны вводиться с клавиатуры.
// Вычислить массив сумм отрицательных чисел в каждой строке.

using System;
using System.Text;
using System.Threading;
using static System.Console;

namespace LaboratoryWork4
{
    class Program
    {

        private static int _x;
        private static int _y;

        // Метод для парсинга string в int.
        // До тех пор, пока вводимая строка не распарсится.
        private static double GetForcedParse(string exceptionMessage, int exceptionMessageShowTime = 1250)
        {
            string inputString;
            double resultDouble;

            while (true)
            {
                if (double.TryParse(inputString = GetLimitedString(8), out resultDouble))
                {
                    WriteLine();
                    return resultDouble;
                }

                ClearString(inputString);

                ForegroundColor = ConsoleColor.Red;
                Write("\a" + exceptionMessage);
                Thread.Sleep(exceptionMessageShowTime);
                ResetColor();

                ClearString(exceptionMessage);
            }  
        }

        // Метод для ограничения длинны вводимой строки.
        // Так же реализует функции клавиш: Enter и Backspace.
        private static string GetLimitedString(int limitOfChars)
        {
            var resultStringBuilder = new StringBuilder(limitOfChars);

            while (resultStringBuilder.Length < limitOfChars)
            {
                ConsoleKeyInfo keyPress = ReadKey();

                switch (keyPress.Key)
                {
                    case ConsoleKey.Enter:
                        return resultStringBuilder.ToString();

                    case ConsoleKey.Backspace:
                        if (string.IsNullOrEmpty(resultStringBuilder.ToString()))
                            break;
                        resultStringBuilder.Remove(resultStringBuilder.Length - 1, 1);

                        // Можно заменить на: Write('\b');
                        SetCursorPosition(_x + resultStringBuilder.Length, _y);
                        Write(' ');
                        SetCursorPosition(_x + resultStringBuilder.Length, _y);
                        break;

                    default:
                        resultStringBuilder.Append(keyPress.KeyChar);
                        break;
                }
            }

            return resultStringBuilder.ToString();
        }

        // Метод для стирания строки.
        // Можно заменить на: Write(new string('\b', stringToClear.Length));
        private static void ClearString(string stringToClear)
        {
            SetCursorPosition(_x, _y);
            Write(new string(' ', stringToClear.Length));
            SetCursorPosition(_x, _y);
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

            // Ввод элементов массива.
            for (var i = 0; i < rows; i++)
            {
                // Перемещение курсора через одну строку для формирования строк.
                _x = 0;
                _y += 2;
                SetCursorPosition(0, _y);

                for (var j = 0; j < columns; j++)
                {
                    inputArray[i, j] = GetForcedParse("Ошибка ввода!");

                    // Перемещение курсора вправо для формирования столбцов.
                    _x += 10;
                    SetCursorPosition(_x, _y);

                    //Создание нового массива исходя из условия задачи.
                    if (inputArray[i, j] < 0)
                        outputArray[i] += inputArray[i, j];
                }
            }

            SetCursorPosition(0, _y + 2);

            ForegroundColor = ConsoleColor.Green;
            WriteLine("Новый массив:\n");

            foreach (var i in outputArray)
                WriteLine(i);

            ResetColor();
            ReadKey(true);
        }
    }
}
