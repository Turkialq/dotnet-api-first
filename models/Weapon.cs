namespace dotnet_api_first.models
{
    public class Weapon
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Damge { get; set; }
        public Character? Character { get; set; }
        public int CharacterId { get; set; }

    }
}