using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_api_first.DTOs.Character;
using dotnet_api_first.models;

namespace dotnet_api_first.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character> {
            new Character(),
            new Character{ID = 1, Name = "Abdullah"},
        };

        private readonly IMapper _mapper;
        public CharacterService(IMapper mapper)
        {
            _mapper = mapper;

        }

        public async Task<ServiceRespinse<List<GetCharacterDTO>>> AddCharacter(AddCharacterDTO newCharacter)
        {
            var serviceResponse = new ServiceRespinse<List<GetCharacterDTO>>();

            characters.Add(_mapper.Map<Character>(newCharacter));
            serviceResponse.Data = characters;
            serviceResponse.Message = "Character added";

            return serviceResponse;

        }

        public async Task<ServiceRespinse<List<GetCharacterDTO>>> GetCharacters()
        {
            var serviceResponse = new ServiceRespinse<List<GetCharacterDTO>>();
            serviceResponse.Data = characters;
            serviceResponse.Message = "Get all characters";

            return serviceResponse;

        }

        public async Task<ServiceRespinse<GetCharacterDTO>> GetSingleCharacter(int id)
        {
            var serviceResponse = new ServiceRespinse<GetCharacterDTO>();
            var character = characters.FirstOrDefault(c => c.ID == id);

            serviceResponse.Data = _mapper.Map<GetCharacterDTO>(character);
            serviceResponse.Message = "Get Single character";

            if (serviceResponse.Data is not null)
            {

                return (serviceResponse);
            }

            throw new Exception("Character not found");
        }


    }
}