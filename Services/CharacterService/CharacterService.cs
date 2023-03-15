using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_api_first.models;

namespace dotnet_api_first.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character> {
            new Character(),
            new Character{ID = 1, Name = "Abdullah"},
        };

        public async Task<ServiceRespinse<List<Character>>> AddCharacter(Character newCharacter)
        {
            var serviceResponse = new ServiceRespinse<List<Character>>();

            characters.Add(newCharacter);
            serviceResponse.Data = characters;
            serviceResponse.Message = "Character added";

            return serviceResponse;

        }

        public async Task<ServiceRespinse<List<Character>>> GetCharacters()
        {
            var serviceResponse = new ServiceRespinse<List<Character>>();
            serviceResponse.Data = characters;
            serviceResponse.Message = "Get all characters";

            return serviceResponse;

        }

        public async Task<ServiceRespinse<Character>> GetSingleCharacter(int id)
        {
            var serviceResponse = new ServiceRespinse<Character>();
            var character = characters.FirstOrDefault(c => c.ID == id);

            serviceResponse.Data = character;
            serviceResponse.Message = "Get Single character";

            if (serviceResponse.Data is not null)
            {

                return (serviceResponse);
            }

            throw new Exception("Character not found");
        }
    }
}