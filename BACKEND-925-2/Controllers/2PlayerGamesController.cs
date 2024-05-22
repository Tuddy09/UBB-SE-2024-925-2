using BACKEND_925_2.Models;
using BACKEND_925_2.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using TwoPlayerGames;
using TwoPlayerGames.Domain.Auxiliary;
using TwoPlayerGames.Repository;

namespace BACKEND_925_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class _2PlayerGamesController : ControllerBase
    {
        private IPlayService playService;
        private InGameService inGameService;
        private PlayerProfileService playerProfileService;

        public _2PlayerGamesController(InGameService inGameService, GamesDbContext gamesDbContext, PlayerProfileService playerProfileService)
        {
            playService = new OfflineGameService();
            this.playerProfileService = playerProfileService;
            this.inGameService = inGameService;
        }

        

        [HttpGet]
        [Route("GetWinner")]
        public IActionResult GetWinner()
        {
            if (playService.GetWinner() == null)
            {
                return StatusCode(StatusCodes.Status204NoContent);
            }
            return Ok(playService.GetWinner());
        }

        [HttpGet]
        [Route("GetTurn")]
        public IActionResult GetTurn()
        {
            return Ok(playService.GetTurn());
        }

        [HttpGet]
        [Route("HasData")]
        public IActionResult HasData()
        {
            return Ok(((OnlineGameService)playService).HasData());
        }

        [HttpGet]
        [Route("PlayOther")]
        public IActionResult PlayOther()
        {
            playService.PlayOther();
            return Ok();
        }

        [HttpGet]
        [Route("IsGameOver")]
        public IActionResult IsGameOver()
        {
            playService.IsGameOver();
            return Ok();
        }

        [HttpGet]
        [Route("SetFirstTurn")]
        public IActionResult SetFirstTurn()
        {
            ((OnlineGameService)playService).SetFirstTurn();
            return Ok();
        }

        [HttpGet]
        [Route("GetStats")]
        public PlayerStats GetStats(Player2PlayerGame player)
        {
            return inGameService.GetStats(player);
        }

        [HttpGet]
        [Route("Play")]
        public IActionResult Play(int numberOfParameters, object[] parameters)
        {
            playService.Play(numberOfParameters, parameters);
            return Ok();
        }

        [HttpGet]
        [Route("GetBoard")]
        public IActionResult GetBoard()
        {
            return Ok(playService.GetBoard());
        }

        [HttpGet]
        [Route("StartPlayer")]
        public IActionResult StartPlayer()
        {
            return Ok(playService.StartPlayer());
        }

        // ...

        [HttpGet]
        [Route("CreatePlayService")]
        public IActionResult CreatePlayService(string playServiceType, object[] parameters)
        {
            Type type = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(t => t.Name == playServiceType);
            if (type == null || !typeof(IPlayService).IsAssignableFrom(type))
            {
                return BadRequest("Invalid play service type");
            }

            try
            {
                playService = (IPlayService)Activator.CreateInstance(type, parameters);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetPlayerStats")]
        public IActionResult GetPlayerStats(Player2PlayerGame player)
        {
            return Ok(playerProfileService.GetProfileStats(player));
        }

        [HttpGet]
        [Route("GetGameStats")]
        public IActionResult GetGameStats(Player2PlayerGame player, string gameType)
        {
            return Ok(playerProfileService.GetGameStats(player, gameType));
        }

        [HttpGet]
        [Route("GetGames")]
        public IActionResult GetGames()
        {
            return Ok(GameStore.Games.Values);
        }
    }
}
