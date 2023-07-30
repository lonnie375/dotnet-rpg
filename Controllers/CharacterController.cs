using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.Models;
using dotnet_rpg.Services.CharacterService;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {

        //Add a static mock character that you can return to the user
        private static List<Character> characters = new List<Character>{
            new Character(), 
            new Character { Id = 1, Name = "Sam"}
        };

        private readonly ICharacterService _characterService; 

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDTO>>>> Get()
        {
            return Ok(await _characterService.GetAllCharacters());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDTO>>> GetSingle(int id){
            return Ok(await _characterService.GetCharacterById(id)); 
        }

        [HttpPost("AddCharacter")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDTO>>>> AddCharacter(AddCharacterDto newCharacter)
        {
            return Ok(await _characterService.AddCharacter(newCharacter)); 
        }
        
        [HttpPut("UpdateCharacter")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDTO>>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            
            var response = await _characterService.UpdateCharacter(updatedCharacter);
            //Updated this so that if the response is null a 404 error is thrown 
            //This is due to the NotFound methodi. 
            if(response.Data is null){
                return NotFound(response);
            }
            return Ok(response);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDTO>>> DeleteCharacter(int id){
            var response = await _characterService.DeleteCharacter(id);
            //Updated this so that if the response is null a 404 error is thrown 
            //This is due to the NotFound methodi. 
            if(response.Data is null){
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
