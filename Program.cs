using System;

namespace Clicker
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            menu.Title = "Gra o Pisanku";
            menu.AddEntry("New Game", () => Game.StartGame() /*NewGame()*/);
            menu.AddEntry("Load Game", () => Game.LoadGame());
            menu.AddEntry("Exit", () => Environment.Exit(0));

            //menu.ShowMenu();

            Game.StartGame();
        }
    }
}