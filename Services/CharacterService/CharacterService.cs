using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg.Data;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Models;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {

        //Add a static mock character that you can return to the user
        private static List<Character> characters = new List<Character>{
            new Character(), 
            new Character { Id = 1, Name = "Sam"}
        };
        private readonly IMapper _mapper; 
        private readonly DataContext _context; 
        public CharacterService(IMapper mapper, DataContext context)
        {   _context = context; 
            _mapper = mapper; 
        }
        public async Task<ServiceResponse<List<GetCharacterDTO>>> AddCharacter(AddCharacterDto newCharacter)
        {
            //Creating a new character instance 
            var character = _mapper.Map<Character>(newCharacter);
            var serviceReponse = new ServiceResponse<List<GetCharacterDTO>>();
            //Taking the max or most recent character, adding 1 to the id and assiging 
            //it to the character that we are adding. 
            character.Id = characters.Max(c => c.Id) + 1; 
            characters.Add(character); 
            serviceReponse.Data = characters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList(); 
            return serviceReponse;
            
        }

        public async Task<ServiceResponse<List<GetCharacterDTO>>> DeleteCharacter(int id)
        {
            var serviceReponse = new ServiceResponse<List<GetCharacterDTO>>(); 
            //We are creating a try catch block here to test our code 
            //And to verify that the user receives a message if they 
            //Insert a character that doesn't exist. 
            try {

            var character = characters.First(c => c.Id == id); 
            if (character is null)
            {
                throw new Exception($"Character with Id '{id}' not found.");
            }

            characters.Remove(character);

            serviceReponse.Data = characters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
            } catch (Exception ex)
            {
                serviceReponse.Success = false; 
                serviceReponse.Message = ex.Message; 
            }
            return serviceReponse; 
            
        }

        public async Task<ServiceResponse<List<GetCharacterDTO>>> GetAllCharacters()
        {
            var serviceReponse = new ServiceResponse<List<GetCharacterDTO>>();
            //Links us to the characters table in our database. 
            var dbCharacters = await _context.Characters.ToListAsync(); 
            serviceReponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList(); 
            return serviceReponse;
        }

        public async Task<ServiceResponse<GetCharacterDTO>> GetCharacterById(int id)
        {
            var serviceReponse = new ServiceResponse<GetCharacterDTO>();
            var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id); 
            serviceReponse.Data = _mapper.Map<GetCharacterDTO>(dbCharacter); 
            return serviceReponse; 

            //We previously received a warning that this could return a value that is null
            //We provided the check below to create a response for when the return
            //value is not null and when it is null. 
        }

        public async Task<ServiceResponse<GetCharacterDTO>> UpdateCharacter(UpdateCharacterDto updateCharacter)
        {
            var serviceReponse = new ServiceResponse<GetCharacterDTO>(); 
            //We are creating a try catch block here to test our code 
            //And to verify that the user receives a message if they 
            //Insert a character that doesn't exist. 
            try {

            var character = characters.FirstOrDefault(c => c.Id == updateCharacter.Id); 
            if (character is null)
            {
                throw new Exception($"Character with Id '{updateCharacter.Id}' not found.");
            }


            character.Name = updateCharacter.Name;
            character.HitPoints = updateCharacter.HitPoints;
            character.Strength = updateCharacter.Strength;
            character.Defense = updateCharacter.Defense;
            character.Intelligence = updateCharacter.Intelligence;
            character.Class = updateCharacter.Class;

            serviceReponse.Data = _mapper.Map<GetCharacterDTO>(character); 
            } catch (Exception ex)
            {
                serviceReponse.Success = false; 
                serviceReponse.Message = ex.Message; 
            }
            return serviceReponse; 
            

        }
    }
}