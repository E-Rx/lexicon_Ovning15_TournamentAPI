using AutoMapper;
using Domain.Models.Entities;
using Tournament.Core.Dtos;


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
