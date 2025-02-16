using System.Text.Json;
using ColDogStudios.RockPaperScissors.src.ShopItems;

namespace ColDogStudios.RockPaperScissors.src.Logic
{
    class Player
    {
        public int Points { get; set; } = 100; // Initial points
        public int Wager { get; set; }
        public Dictionary<string, int> PurchasedItems { get; set; } = [];

        private static string GetFilePath()
        {
            string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string folderPath = Path.Combine(localAppData, "ColDog Studios", "Rock Paper Scissors");
            Directory.CreateDirectory(folderPath);
            return Path.Combine(folderPath, "playerStats.json");
        }

        public void SavePlayerStats()
        {
            string filePath = GetFilePath();
            string json = JsonSerializer.Serialize(this);
            File.WriteAllText(filePath, json);
        }

        public static Player LoadPlayerStats()
        {
            string filePath = GetFilePath();
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<Player>(json) ?? new Player();
            }
            return new Player();
        }

        public void PurchaseItem(ShopItem item)
        {
            if (Points < item.Price)
            {
                Console.WriteLine("You do not have enough points to purchase this item.");
                return;
            }

            if (PurchasedItems.ContainsKey(item.Name))
            {
                if (item.Tier > PurchasedItems[item.Name] + 1)
                {
                    Console.WriteLine($"You must purchase {item.Name} Tier {PurchasedItems[item.Name] + 1} before purchasing Tier {item.Tier}.");
                    return;
                }
            }
            else if (item.Tier != 1)
            {
                Console.WriteLine($"You must purchase {item.Name} Tier 1 before purchasing Tier {item.Tier}.");
                return;
            }

            Points -= item.Price;
            PurchasedItems[item.Name] = item.Tier;
            Console.WriteLine($"You purchased {item.Name} Tier {item.Tier}!");
        }
    }
}
