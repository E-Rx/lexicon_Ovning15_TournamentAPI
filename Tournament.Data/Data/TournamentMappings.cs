using AutoMapper;
using Tournament.Core.Dtos;
using Tournament.Core.Entities;

namespace Tournament.Data.Data
{
  
public class TournamentMappings : Profile
    {
        public TournamentMappings()
        {
            CreateMap<TournamentDetails, TournamentDetailsDto>().ReverseMap();
            CreateMap<Game, GameDto>().ReverseMap();
            CreateMap<TournamentDetails, TournamentDetailsUpdateDto>().ReverseMap();
            CreateMap<Game, GameUpdateDto>().ReverseMap();

        }
    }
    
   
}
