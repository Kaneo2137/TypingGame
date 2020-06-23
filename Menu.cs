using System;
using System.Collections.Generic;

namespace Clicker
{
    class Menu
    {
        string title = "MY EPIC GAME";
        ConsoleColor titleColor = ConsoleColor.Gray; // Standard console font color

        private bool repeat;

        private bool hasBack;
        public bool HasBack
        {
            get { return hasBack; }
            set { hasBack = value; }
        }

        List<string> MenuEntriesNames = new List<string>();

        List<Action> MenuEntriesActions = new List<Action>();

        public Menu() { }

        public Menu(string title)
        {
            this.title = title;
        }

        public Menu(string title, ConsoleColor color)
        {
            this.title = title;
            titleColor = color;
        }

        public void ShowMenu()
        {
            int selectedOption = 0;
            repeat = true;

            if (hasBack)
            {
                AddEntry("Back", () => repeat = false);
            }

            while (repeat)
            {
                Console.Clear();

                if (!string.IsNullOrEmpty(title))
                    ExtendedConsole.WriteLine(title, titleColor);

                foreach (var option in MenuEntriesNames)
                {
                    System.Console.WriteLine(option);
                }

                if (TrySelectOption(ref selectedOption))
                    MenuEntriesActions[selectedOption]();
            }
        }

        private bool TrySelectOption(ref int selectedOption)
        {
            if (MenuEntriesNames.Count == 0 || MenuEntriesActions.Count == 0) return true;

            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.UpArrow:
                    if (selectedOption == 0) { break; }

                    MenuEntriesNames[selectedOption] = " " + MenuEntriesNames[selectedOption].Remove(0, 1);
                    --selectedOption;
                    MenuEntriesNames[selectedOption] = ">" + MenuEntriesNames[selectedOption].Remove(0, 1);
                    break;

                case ConsoleKey.DownArrow:
                    if (selectedOption == MenuEntriesNames.Count - 1) { break; }

                    MenuEntriesNames[selectedOption] = " " + MenuEntriesNames[selectedOption].Remove(0, 1);
                    ++selectedOption;
                    MenuEntriesNames[selectedOption] = ">" + MenuEntriesNames[selectedOption].Remove(0, 1);
                    break;

                case ConsoleKey.Enter:
                    return true;

                default:
                    return false;
            }
            return false;
        }
        
        public void AddEntry(string name, Action action)
        {
            if (MenuEntriesNames.Count != 0)
                MenuEntriesNames.Add("  " + name);
            else
                MenuEntriesNames.Add("> " + name);
            MenuEntriesActions.Add(action);
        }
    }
}