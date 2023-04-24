using dotnet_api_first.models;
using dotnet_api_first.Services.CharacterService;
using Microsoft.AspNetCore.Mvc;
using dotnet_api_first.DTOs.Character;
using Microsoft.AspNetCore.Authorization;

namespace dotnet_api_first.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {

        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet("characters")]
        public async Task<ActionResult<ServiceRespinse<List<GetCharacterDTO>>>> GetListOfCharacters()
        {
            return Ok(await _characterService.GetCharacters());
        }

        [HttpGet("character/{id}")]
        public async Task<ActionResult<ServiceRespinse<GetCharacterDTO>>> GetSingleCaracter(int id)
        {
            return Ok(await _characterService.GetSingleCharacter(id));
        }

        [HttpPost("character")]
        public async Task<ActionResult<ServiceRespinse<List<GetCharacterDTO>>>> AddChacater(AddCharacterDTO newChar)
        {
            return Ok(await _characterService.AddCharacter(newChar));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceRespinse<List<GetCharacterDTO>>>> UpdateCharacter(UpdateCharacterDTO newChar)
        {
            var response = await _characterService.UpdateCharacter(newChar);
            if (response.Data is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpDelete("character/{id}")]

        public async Task<ActionResult<ServiceRespinse<List<GetCharacterDTO>>>> DeleteCharacter(int id)
        {
            var response = await _characterService.DeleteCharacter(id);
            if (response.Data is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

    }




}
