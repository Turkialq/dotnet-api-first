using System.Security.Claims;
using AutoMapper;
using dotnet_api_first.Data;
using dotnet_api_first.DTOs.Character;
using dotnet_api_first.Services.Chache;
using dotnet_api_first.models;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace dotnet_api_first.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContex _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICacheService _cacheService;
        public CharacterService(IMapper mapper, DataContex context, IHttpContextAccessor httpContextAccessor, ICacheService cacheService)
        {
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _cacheService = cacheService;

        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext!.User
        .FindFirstValue(ClaimTypes.NameIdentifier)!);

        public async Task<ServiceRespinse<List<GetCharacterDTO>>> AddCharacter(AddCharacterDTO newCharacter)
        {
            var serviceResponse = new ServiceRespinse<List<GetCharacterDTO>>();

            var character = _mapper.Map<Character>(newCharacter);
            character.user = await _context.users.FirstOrDefaultAsync(u => u.id == GetUserId());

            await _context.characters.AddAsync(character);
            await _context.SaveChangesAsync();

            serviceResponse.Data = await _context.characters.Where(u => u.user!.id == GetUserId()).Select(c => _mapper.Map<GetCharacterDTO>(c)).ToListAsync();
            serviceResponse.Message = "Character added";

            return serviceResponse;

        }

        public async Task<ServiceRespinse<List<GetCharacterDTO>>> GetCharacters()
        {
            var serviceResponse = new ServiceRespinse<List<GetCharacterDTO>>();
            // Get From Redis If Avalible
            var cachedResult = _cacheService.GetData<IEnumerable<Character>>(GetUserId().ToString());
            if (cachedResult is not null && cachedResult.Count() > 0)
            {
                serviceResponse.Data = cachedResult.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
                serviceResponse.Message = "Get all characters from redis";
            }
            else
            {
                var dbCharacters = await _context.characters.Where(c => c.user!.id == GetUserId()).ToListAsync();
                serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
                serviceResponse.Message = "Get all characters";

                var expiryTime = DateTimeOffset.Now.AddSeconds(30);
                _cacheService.SetData<IEnumerable<Character>>(GetUserId().ToString(), dbCharacters, expiryTime);
            }

            return serviceResponse;
        }

        public async Task<ServiceRespinse<GetCharacterDTO>> GetSingleCharacter(int id)
        {
            var serviceResponse = new ServiceRespinse<GetCharacterDTO>();
            var character = await _context.characters.Where(c => c.user!.id == GetUserId()).FirstOrDefaultAsync(c => c.ID == id);

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
            var updatedCharacter = await _context.characters.Include(c => c.user)
            .Where(u => u.user!.id == GetUserId()).FirstAsync(c => c.ID == newCharacter.ID);

            try
            {
                if (updatedCharacter is null)
                {
                    throw new Exception($"Character with ID : '{newCharacter.ID}' not found");
                }

                updatedCharacter.Agility = newCharacter.Agility;
                updatedCharacter.Name = newCharacter.Name;
                updatedCharacter.HitPoints = newCharacter.HitPoints;

                await _context.SaveChangesAsync();


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

        public async Task<ServiceRespinse<List<GetCharacterDTO>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceRespinse<List<GetCharacterDTO>>();

            var character = await _context.characters.Where(u => u.user!.id == GetUserId()).FirstAsync(c => c.ID == id);

            if (character is not null)
            {
                var result = id;
                _context.characters.Remove(character);
                await _context.SaveChangesAsync();
                serviceResponse.Data = await _context.characters.Where(u => u.user!.id == GetUserId()).Select(c => _mapper.Map<GetCharacterDTO>(c)).ToListAsync();
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