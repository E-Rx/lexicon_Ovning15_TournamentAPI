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
            CreateMap<TournamentDetails, TournamentDetailsUpdateDto>().ReverseMap();
            CreateMap<TournamentDetails, TournamentDetailsCreateDto>().ReverseMap();


            CreateMap<Game, GameDto>().ReverseMap();
            CreateMap<Game, GameUpdateDto>().ReverseMap();
        }
    }
    
   
}
