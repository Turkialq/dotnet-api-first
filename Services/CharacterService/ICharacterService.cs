using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_api_first.models;
using dotnet_api_first.DTOs.Character;

namespace dotnet_api_first.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceRespinse<List<GetCharacterDTO>>> GetCharacters();
        Task<ServiceRespinse<GetCharacterDTO>> GetSingleCharacter(int id);
        Task<ServiceRespinse<List<GetCharacterDTO>>> AddCharacter(AddCharacterDTO newCharacter);
        Task<ServiceRespinse<GetCharacterDTO>> UpdateCharacter(UpdateCharacterDTO newCharacter);


    }
}