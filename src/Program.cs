using ColDogStudios.RockPaperScissors.src.Logic;
using ColDogStudios.RockPaperScissors.src.Menus;

namespace ColDogStudios.RockPaperScissors.src
{
    class Program
    {
        static void Main(string[] args)
        {
            bool developerMode = args.Length > 0 && args[0] == "--dev";
            Player player = Player.LoadPlayerStats();
            MainMenu.ShowMainMenu(player, developerMode);
        }
    }
}
