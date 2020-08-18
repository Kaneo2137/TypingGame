namespace Clicker
{
    class Player
    {
        public enum KeyboardLayout
        {
            QWERTY,
            QWERTZ,
            Colemak,
            ColemakDHMod,
            Dvorak
        }

        private string saveFile = "save.dat";

        public string Name { get; set; }
        public int Score { get; set; }
        public KeyboardLayout Layout { get; set; }
        public int[] UnlockedSongs { get; set; }

        public void SavePlayer()
        {

        }
    }
}