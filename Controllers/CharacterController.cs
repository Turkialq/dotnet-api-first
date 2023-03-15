using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_api_first.models;
using dotnet_api_first.Services.CharacterService;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_api_first.Controllers
{
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
        public async Task<ActionResult<ServiceRespinse<List<Character>>>> GetListOfCharacters()
        {
            return Ok(await _characterService.GetCharacters());
        }

        [HttpGet("character/{id}")]
        public async Task<ActionResult<ServiceRespinse<Character>>> GetSingleCaracter(int id)
        {
            return Ok(await _characterService.GetSingleCharacter(id));
        }

        [HttpPost("character")]
        public async Task<ActionResult<ServiceRespinse<List<Character>>>> AddChacater(Character newChar)
        {
            return Ok(await _characterService.AddCharacter(newChar));
        }

    }
}