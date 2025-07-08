using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tournament.Core.Dtos;
using Tournament.Core.QueryParameters;
using Domain.Contracts.Repositories;
using Domain.Models.Entities;
using Service.Contracts;
using Tournament.Services.Services;

namespace Tournament.Presentation.Controllers
{
    [Route("api/TournamentDetails")]
    [ApiController]
    public class TournamentDetailsController : ControllerBase
    {
        //private readonly IUnitOfWork _unitOfWork;
        //private readonly IMapper _mapper;
        private readonly IServiceManager _serviceManager;

        public TournamentDetailsController(IServiceManager serviceManager)
        {
            //_unitOfWork = unitOfWork;
            //_mapper = mapper;
            _serviceManager = serviceManager;
        }

        // GET: api/TournamentDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TournamentDetailsDto>>> GetAllTournamentDetails([FromQuery] TournamentDetailsQuery query)
        {
            try
            {
                var tournaments = await _serviceManager.TournamentDetailsService.GetAllTournamentsAsync(query);
                if (tournaments?.Items == null || !tournaments.Items.Any())
                {
                    return NotFound("No tournament details found.");
                }

                return Ok(tournaments);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while retrieving tournaments.");
            }
        }

        // GET: api/TournamentDetails/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TournamentDetailsDto>> GetTournamentDetails(int id)
        {
            try
            {
                var query = new TournamentDetailsQuery { Id = id };
                var tournamentDetails = await _serviceManager.TournamentDetailsService.GetTournamentDetailsAsync(query);

                if (tournamentDetails == null)
                {
                    return NotFound($"Tournament with ID {id} not found.");
                }

                return Ok(tournamentDetails);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while retrieving the tournament.");
            }
        }

        // PUT: api/TournamentDetails/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTournamentDetails(int id, TournamentDetailsUpdateDto tournamentDetailsUpdateDto)
        {
            if (id != tournamentDetailsUpdateDto.Id)
            {
                return BadRequest("ID in URL does not match ID in body.");
            }

            try
            {
                var success = await _serviceManager.TournamentDetailsService.UpdateTournamentAsync(tournamentDetailsUpdateDto);

                if (!success)
                {
                    return NotFound($"Tournament with ID {id} no longer exists.");
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "A concurrency error occurred while updating.");
            }
            

            return NoContent();
        }

        // POST: api/TournamentDetails
        [HttpPost]
        public async Task<ActionResult<TournamentDetailsDto>> PostTournamentDetails(TournamentDetailsCreateDto tournamentDetailsCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdTournament = await _serviceManager.TournamentDetailsService.CreateTournamentAsync(tournamentDetailsCreateDto);
                if (createdTournament == null)
                {
                    return StatusCode(500, "Failed to save tournament to the database.");
                }

                return CreatedAtAction(nameof(GetTournamentDetails), new { id = createdTournament.Id }, createdTournament);
            }
            catch (Exception)
            {
                return StatusCode(500, "Failed to save tournament to the database.");
            }
        }


        // PATCH: api/TournamentDetails/5
        [HttpPatch("{tournamentDetailsId}")]
        public async Task<ActionResult> PatchTournamentDetails(int tournamentDetailsId, JsonPatchDocument<TournamentDetailsUpdateDto> patchDocument)
        {
            if (patchDocument is null)
            {
                return BadRequest("No patch document provided.");
            }

            var success = await _serviceManager.TournamentDetailsService.PatchTournamentDetails(tournamentDetailsId, patchDocument);
            if (!success)
            {
                return NotFound($"Tournament with ID {tournamentDetailsId} not found.");
            }

            return NoContent();
        }

        // DELETE: api/TournamentDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournamentDetails(int id)
        {
            try
            {
                var success = await _serviceManager.TournamentDetailsService.DeleteTournamentAsync(id);
                if (!success)
                {
                    return NotFound($"Tournament with ID {id} not found.");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while deleting the tournament.");
            }

            return NoContent();
        }

       
    }
}
