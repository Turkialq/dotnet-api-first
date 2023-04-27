using dotnet_api_first.DTOs.Character;
using dotnet_api_first.DTOs.Weapon;
using dotnet_api_first.models;

namespace dotnet_api_first.Services.WeaponService
{
    public class WeaponService : IWeaponService
    {
        public Task<ServiceRespinse<GetCharacterDTO>> AddWeapon(AddWeaponDTO newWeapon)
        {
            throw new NotImplementedException();
        }
    }
}