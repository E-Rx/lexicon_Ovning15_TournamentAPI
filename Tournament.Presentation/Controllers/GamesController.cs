using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tournament.Core.Dtos;
using Domain.Contracts.Repositories;
using Domain.Models.Entities;
using Tournament.Core.QueryParameters;
using Service.Contracts;

namespace Tournament.Presentation.Controllers
{
    [Route("api/Games")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        //private readonly IUnitOfWork _unitOfWork;
        //private readonly IMapper _mapper;

        private readonly IServiceManager _serviceManager;

        public GamesController(IServiceManager serviceManager)
        {
            //_unitOfWork = unitOfWork;
           // _mapper = mapper;
            _serviceManager=serviceManager;
        }

        // GET: api/Games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameDto>>> GetGame([FromQuery] GameQuery gameQuery)
        {
            try
            {
                var games = await _serviceManager.GameService.GetAllGamesAsync(gameQuery);


                if (games == null || !games.Any())
                {
                    return NotFound("No games found.");
                }


                return Ok(games);
            }

            catch (Exception)
            {
                return StatusCode(500, "An error occurred while retrieving games.");
            }
        }

        // GET: api/Games/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GameDto>> GetGame(int id)
        {
            try
            {
                var game = await _serviceManager.GameService.GetGameAsync(id);

                if (game == null)
                {
                    return NotFound($"Game with ID {id} not found.");
                }

                return Ok(game);
            }

            catch (Exception)
            {
                return StatusCode(500, "An error occurred while retrieving the game.");
            }
        }

        //// GET: api/Games/search?title=Finale
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<GameDto>>> GetGameByTitle([FromQuery] string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return BadRequest("You must provide a title to search.");
            }

            try
            {
                var matchingGames = await _serviceManager.GameService.GetGameByTitleAsync(title);

                if (!matchingGames.Any())
                {
                    return NotFound($"No games found with the title '{title}'.");
                }

                return Ok(matchingGames);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while searching for games.");
            }
        }



        // PUT: api/Games/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame(int id, GameUpdateDto gameUpdateDto)
        {
            if (id != gameUpdateDto.Id)
            {
                return BadRequest("ID in URL does not match ID in body.");
            }

            try
            {
                var success = await _serviceManager.GameService.UpdateGameAsync(gameUpdateDto);

                if (!success)
                {
                    return NotFound($"Game with ID {id} does not exist.");
                }

                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "A concurrency error occurred while updating the game.");
            }

        }

        // POST: api/Games
        [HttpPost]
        public async Task<ActionResult<GameDto>> PostGame(GameCreateDto gameCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            try
            {
                var createdGame = await _serviceManager.GameService.CreateGameAsync(gameCreateDto);

                if (createdGame == null)
                {
                    return StatusCode(500, "Failed to save game to the database.");
                }

                return CreatedAtAction(nameof(GetGame), new { id = createdGame.Id }, createdGame);
            }
            catch (Exception)
            {
                return StatusCode(500, "Failed to save game to the database.");
            }

        }


        [HttpPatch("{gameId}")]
        public async Task<ActionResult> PatchGame(int gameId, JsonPatchDocument<GameUpdateDto> patchDocument)
        {
            if (patchDocument is null)
            {
                return BadRequest("No patch document provided.");
            }

            var success = await _serviceManager.GameService.PatchGameAsync(gameId, patchDocument);

            if (!success)
            {
                return NotFound($"Game with ID {gameId} not found.");
            }

            return NoContent();
        }


        // DELETE: api/Games/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            try
            {
                var success = await _serviceManager.GameService.DeleteGameAsync(id);

                if (!success)
                {
                    return NotFound($"Game with ID {id} not found.");
                }

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while deleting the game.");
            }
        }

       
    }
}
