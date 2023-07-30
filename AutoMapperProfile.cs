using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks;
using AutoMapper;

namespace dotnet_rpg
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //This MAP worked for the first two mmethods 
            CreateMap<Character, GetCharacterDTO>();

            //We need this for the method used to create another character 
            CreateMap<AddCharacterDto, Character>();
        }
    }
}