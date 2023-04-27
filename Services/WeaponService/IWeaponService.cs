using dotnet_api_first.DTOs.Character;
using dotnet_api_first.DTOs.Weapon;
using dotnet_api_first.models;

namespace dotnet_api_first.Services.WeaponService
{
    public interface IWeaponService
    {
        Task<ServiceRespinse<GetCharacterDTO>> AddWeapon(AddWeaponDTO newWeapon);

    }
}