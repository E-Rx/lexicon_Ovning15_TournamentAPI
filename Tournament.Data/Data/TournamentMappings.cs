using AutoMapper;
using Domain.Models.Entities;
using Tournament.Core.Dtos;


namespace Tournament.Data.Data
{
  
public class TournamentMappings : Profile
    {
        public TournamentMappings()
        {
            CreateMap<TournamentDetails, TournamentDetailsDto>();
            CreateMap<TournamentDetails, TournamentDetailsUpdateDto>().ReverseMap();
            CreateMap<TournamentDetails, TournamentDetailsCreateDto>().ReverseMap();


            CreateMap<Game, GameDto>();
            CreateMap<Game, GameUpdateDto>().ReverseMap();
            CreateMap<Game, GameCreateDto>().ReverseMap();

            // for PUT method
           // CreateMap<GameDto, Game>()
           // .ForMember(dest => dest.Id, opt => opt.Ignore());
           

        }
    }
    
   
}
