using System;
using System.IO;
using System.Linq;

namespace Clicker
{
    static class Game
    {
        static int score = 0;
        static string[] wordlist;
        static Random rnd;

        static Game()
        {
            wordlist = File.ReadAllLines("wordlist.txt").ToArray();
            rnd = new Random();
        }

        public static void NewGame()
        {

        }

        public static void LoadGame()
        {
            Menu selectSave = new Menu("Select game save:");

            //foreach()
        }

        public static void StartGame()
        {

            var word = wordlist[rnd.Next(wordlist.Length)];
            var secondWord = wordlist[rnd.Next(wordlist.Length)];

            while (true)
            {
                byte errors = 0;
                var thirdWord = wordlist[rnd.Next(wordlist.Length)];
                string input = string.Empty;

                Console.Clear();

                ExtendedConsole.WriteLine(" Score: " + score, ConsoleColor.Blue);
                Console.WriteLine();

                Console.ResetColor();

                Console.WriteLine($"{word} {secondWord} {thirdWord}");
                Console.WriteLine();

                do
                {
                    var key = Console.ReadKey(false);

                    if (key.Key == ConsoleKey.Escape)
                        return;

                    Console.SetCursorPosition(0, Console.CursorTop);

                    if (key.Key == ConsoleKey.Backspace)
                    {
                        if (input != string.Empty || input != "")
                        {
                            input = new string(input.ToArray().SkipLast(1).ToArray());
                            ColorErrors(word, input, ref errors);
                        }
                    }
                    else
                    {
                        input += key.KeyChar;
                        ColorErrors(word, input, ref errors);
                    }

                    ExtendedConsole.ClearLine();

                    Console.Write(input);

                } while (input != word);

                word = secondWord;
                secondWord = thirdWord;

                score += word.Length - errors;
            }
        }

        private static void ColorErrors(string word, string input, ref byte errors)
        {
            if (word.Length < input.Length)
                return;

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] != word[i])
                {
                    errors++;
                    Console.ForegroundColor = ConsoleColor.Red;
                    return;
                }
            }

            Console.ResetColor();
        }
    }
}