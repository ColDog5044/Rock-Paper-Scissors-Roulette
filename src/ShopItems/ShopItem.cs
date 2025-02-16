namespace ColDogStudios.RockPaperScissors.src.ShopItems
{
    abstract class ShopItem(string name, int price, int tier)
    {
        public string Name { get; set; } = name;
        public int Price { get; set; } = price;
        public int Tier { get; set; } = tier;
    }
}
