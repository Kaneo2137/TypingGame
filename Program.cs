using System;

namespace Clicker
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu("Gra o Pisanku");
            menu.AddEntry("New Game", () => Game.NewGame());
            menu.AddEntry("Load Game", () => Game.LoadGame());
            menu.AddEntry("Exit", () => Environment.Exit(0));

            menu.ShowMenu();
        }
    }
}