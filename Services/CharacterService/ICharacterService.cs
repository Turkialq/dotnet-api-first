using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_api_first.models;

namespace dotnet_api_first.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceRespinse<List<Character>>> GetCharacters();
        Task<ServiceRespinse<Character>> GetSingleCharacter(int id);
        Task<ServiceRespinse<List<Character>>> AddCharacter(Character newCharacter);


    }
}