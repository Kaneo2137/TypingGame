using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace Clicker
{
    static class Game
    {
        static private Player player;
        static private int score;
        static private string[] wordlist;
        static private Random rnd;

        static Game()
        {
            wordlist = GetTextFromAssembly(); //File.ReadAllLines("wordlist.txt").ToArray();
            rnd = new Random();
        }

        static string[] GetTextFromAssembly()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "Clicker.wordlist.txt";
            List<string> lines = new List<string>();

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                while(!reader.EndOfStream)
                {
                    lines.Add(reader.ReadLine());
                }
            }

            return lines.ToArray();
        }

        public static void NewGame()
        {
            player = new Player();

            Console.WriteLine("What's your nick?");
            Console.Write("> ");

            player.Name = Console.ReadLine();

            Console.Clear();

            var askLayout = new Menu();
            askLayout.Title = "What's your layout?";

            askLayout.OnExit = () => { }; // To implement

            foreach (var layout in (Player.KeyboardLayout[])Enum.GetValues(typeof(Player.KeyboardLayout)))
            {
                askLayout.AddEntry(layout.ToString(), () => { player.Layout = layout; askLayout.Repeat = false; });
            }

            askLayout.ShowMenu();
        }

        public static void LoadGame()
        {
            if (!Directory.Exists("save"))
                Directory.CreateDirectory("save");

            if (!File.Exists("save/save.sav"))
                File.Create("save/save.sav");

            var rawSaves = File.ReadAllLines("save/save.sav");

            if (rawSaves.Length > 0)
            {
                if (rawSaves[0].Split('^').Length < 3)
                {
                    File.WriteAllLines($"save/corrupted_save_{DateTime.Now}.dat", rawSaves);
                    File.Create("save/save.dat");
                    PopulateSave();
                }
                else
                {
                    // Some stuff
                }
            }
            else
                PopulateSave();

        }

        private static void PopulateSave()
        {

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

                Console.WriteLine($"{word} {secondWord} {thirdWord}");
                Console.WriteLine();

                do
                {
                    var key = Console.ReadKey(true);

                    if (key.Key == ConsoleKey.Escape)
                        return;

                    if (key.Key == ConsoleKey.Backspace && !string.IsNullOrEmpty(input))
                    {
                        input = new String(input.SkipLast(1).ToArray());
                        ExtendedConsole.DelOneCharacter();
                        ColorErrors(word, input, ref errors);
                    }
                    else
                    {
                        input += key.KeyChar;
                        ColorErrors(word, input, ref errors);
                        Console.Write(input.Last());
                    }

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