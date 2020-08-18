using System;

namespace Clicker
{
    public static class ExtendedConsole
    {
        public static void WriteLine(object obj, ConsoleColor color)
        {
            var previousColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(obj.ToString());
            Console.ForegroundColor = previousColor;
        }

        public static void ClearLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.BufferWidth - 1)); // Preventing buffer overflow
            Console.SetCursorPosition(0, Console.CursorTop);
        }

        public static void DelOneCharacter()
        {
            if(Console.CursorLeft == 0)
                return;
            
            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
            Console.Write(' ');
            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
        }
    }
}