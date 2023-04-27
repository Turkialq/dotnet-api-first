namespace dotnet_api_first.models
{
    public class Character
    {
        public int ID { get; set; }
        public string Name { get; set; } = "Turki";
        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Defence { get; set; } = 10;
        public int Agility { get; set; } = 10;
        public RpgClass Class { get; set; } = RpgClass.Paladin;
        public User? user { get; set; }
        public Weapon? Weapon { get; set; }


    }
}