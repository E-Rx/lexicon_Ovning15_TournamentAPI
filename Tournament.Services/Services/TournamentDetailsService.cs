using AutoMapper;
using Domain.Contracts.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using Service.Contracts;
using Tournament.Core.Dtos;
using Tournament.Core.QueryParameters;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Tournament.Services.Services
{
    public class TournamentDetailsService : ITournamentDetailsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TournamentDetailsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TournamentDetailsPagedDto> GetAllTournamentsAsync(TournamentDetailsQuery query)
        {
            var totalItems = await _unitOfWork.TournamentDetailsRepository.GetCountAsync();
            var tournaments = await _unitOfWork.TournamentDetailsRepository.GetAllAsync(
                query.IncludeGames, query.SortBy, query.PageNumber, query.PageSize);

            var tournamentDetailsDtos = _mapper.Map<List<TournamentDetailsDto>>(tournaments);

            var totalPages = (int)Math.Ceiling((double)totalItems / query.PageSize);

            return new TournamentDetailsPagedDto
            {
                Items = tournamentDetailsDtos,
                TotalCount = totalItems,
                TotalPages = totalPages,           
                CurrentPage = query.PageNumber,   
                PageSize = query.PageSize          
            };
        }


        public async Task<TournamentDetailsDto?> GetTournamentDetailsAsync(TournamentDetailsQuery query)
        {
            var tournament = await _unitOfWork.TournamentDetailsRepository.GetAsync(query.Id);
            return tournament == null ? null : _mapper.Map<TournamentDetailsDto>(tournament);
        }

        public async Task<TournamentDetailsDto> CreateTournamentAsync(TournamentDetailsCreateDto tournamentDetailsCreateDto)
        {
            var tournament = _mapper.Map<TournamentDetails>(tournamentDetailsCreateDto);
            _unitOfWork.TournamentDetailsRepository.Add(tournament);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<TournamentDetailsDto>(tournament);
        }


        public async Task<bool> UpdateTournamentAsync(TournamentDetailsUpdateDto tournamentDetailsUpdateDto)
        {
            var existing = await _unitOfWork.TournamentDetailsRepository.GetAsync(tournamentDetailsUpdateDto.Id);
            if (existing == null) return false;

            _mapper.Map(tournamentDetailsUpdateDto, existing);
            _unitOfWork.TournamentDetailsRepository.Update(existing);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> DeleteTournamentAsync(int id)
        {
            var tournamentDetails = await _unitOfWork.TournamentDetailsRepository.GetAsync(id);
            if (tournamentDetails == null)
            {
                return false;
            }

            _unitOfWork.TournamentDetailsRepository.Remove(tournamentDetails);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> PatchTournamentDetails(int id, JsonPatchDocument<TournamentDetailsUpdateDto> patchDocument)
        {
            var tournament = await _unitOfWork.TournamentDetailsRepository.GetAsync(id);
            if (tournament == null)
                return false;

            var dtoToPatch = _mapper.Map<TournamentDetailsUpdateDto>(tournament);
            patchDocument.ApplyTo(dtoToPatch);


            _mapper.Map(dtoToPatch, tournament);
            await _unitOfWork.CompleteAsync();

            return true;
        }

       
    }
}
