using System;
using System.Collections.Generic;

namespace Clicker
{
    class Menu
    {
        private string title;
        public string Title
        {
            get => title;
            set => title = value;
        }

        private ConsoleColor titleColor;
        public ConsoleColor TitleColor
        {
            get => titleColor;
            set => titleColor = value;
        }

        private bool hasBack;
        public bool HasBack
        {
            get => hasBack;
            set => hasBack = value;
        }
        
        private bool repeat;
        /// <summary>
        /// Set this property to false to exit from ShowMenu loop
        /// </summary>
        /// <value>True</value>
        public bool Repeat
        {
            get => repeat;
            set => repeat = value;
        }

        public Action OnExit;

        private List<Entry> MenuEntries = new List<Entry>();

        public Menu()
        {
            repeat = true;
            titleColor = ConsoleColor.Gray; // Standard console font color
        }

        public void ShowMenu()
        {
            int selectedOption = 0;

            if (hasBack)
            {
                AddEntry("Back", () => repeat = false);
            }

            do
            {
                Console.Clear();

                if (!string.IsNullOrEmpty(title))
                    ExtendedConsole.WriteLine(title, titleColor);

                foreach (var option in MenuEntries)
                {
                    System.Console.WriteLine(option.Name);
                }

                if (TrySelectOption(ref selectedOption))
                    MenuEntries[selectedOption].Action();

                if (!repeat)
                    OnExit();

            } while (repeat);
        }

        private bool TrySelectOption(ref int selectedOption)
        {
            if (MenuEntries.Count == 0) return true;

            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.UpArrow:
                    if (selectedOption == 0) { break; }

                    MenuEntries[selectedOption].Name = " " + MenuEntries[selectedOption].Name.Remove(0, 1);
                    --selectedOption;
                    MenuEntries[selectedOption].Name = ">" + MenuEntries[selectedOption].Name.Remove(0, 1);
                    break;

                case ConsoleKey.DownArrow:
                    if (selectedOption == MenuEntries.Count - 1) { break; }

                    MenuEntries[selectedOption].Name = " " + MenuEntries[selectedOption].Name.Remove(0, 1);
                    ++selectedOption;
                    MenuEntries[selectedOption].Name = ">" + MenuEntries[selectedOption].Name.Remove(0, 1);
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
            if (MenuEntries.Count != 0)
                MenuEntries.Add(new Entry { Name = "  " + name, Action = action });
            else
                MenuEntries.Add(new Entry { Name = "> " + name, Action = action });
        }

        private class Entry
        {
            public string Name { get; set; }
            public Action Action { get; set; }
        }
    }
}