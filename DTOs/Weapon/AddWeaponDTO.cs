namespace dotnet_api_first.DTOs.Weapon
{
    public class AddWeaponDTO
    {
        public string Name { get; set; } = string.Empty;
        public int Damge { get; set; }
        public int CharacterId { get; set; }


    }
}