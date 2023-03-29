using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_api_first.Data;
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
        private readonly DataContex _context;
        public CharacterService(IMapper mapper, DataContex context)
        {
            _mapper = mapper;
            _context = context;

        }

        public async Task<ServiceRespinse<List<GetCharacterDTO>>> AddCharacter(AddCharacterDTO newCharacter)
        {
            var serviceResponse = new ServiceRespinse<List<GetCharacterDTO>>();

            var character = _mapper.Map<Character>(newCharacter);
            character.ID = characters.Max(c => c.ID) + 1;

            _context.Add(character);
            await _context.SaveChangesAsync();

            characters.Add(character);

            serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
            serviceResponse.Message = "Character added";

            return serviceResponse;

        }

        public async Task<ServiceRespinse<List<GetCharacterDTO>>> GetCharacters()
        {
            var serviceResponse = new ServiceRespinse<List<GetCharacterDTO>>();
            serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
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

        public async Task<ServiceRespinse<GetCharacterDTO>> UpdateCharacter(UpdateCharacterDTO newCharacter)
        {
            var serviceResponse = new ServiceRespinse<GetCharacterDTO>();
            var updatedCharacter = characters.FirstOrDefault(c => c.ID == newCharacter.ID);

            try
            {
                if (updatedCharacter is null)
                {
                    throw new Exception($"Character with ID : '{newCharacter.ID}' not found");
                }

                updatedCharacter.Agility = newCharacter.Agility;
                updatedCharacter.Name = newCharacter.Name;
                updatedCharacter.HitPoints = newCharacter.HitPoints;


                serviceResponse.Data = _mapper.Map<GetCharacterDTO>(updatedCharacter);
                serviceResponse.Message = "Update Single character";
            }
            catch (Exception e)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = e.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceRespinse<GetCharacterDTO>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceRespinse<GetCharacterDTO>();

            // get the index of the object
            // then delete it

            int indexOfDeletedItem = characters.FindIndex(c => c.ID == id);

            if (indexOfDeletedItem >= 0)
            {
                characters.RemoveAt(indexOfDeletedItem);
                serviceResponse.Data = _mapper.Map<GetCharacterDTO>(characters[indexOfDeletedItem]);
                serviceResponse.Message = "deleted Single character";

                return serviceResponse;

            }
            else
            {
                throw new Exception($"Character With ID :'{id}' not found");
            }

        }
    }
}