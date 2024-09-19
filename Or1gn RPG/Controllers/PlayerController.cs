using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Or1gn_RPG.Controllers {

    [ApiController]
    [Route("api/player")]
    public class PlayerController : ControllerBase {
        public IPlayerInfoService PlayerService { get; }
        public PlayerController(IPlayerInfoService playerService) {
            PlayerService = playerService;
        }

        [HttpGet("getplayers")]
        public IActionResult GetPlayers([Required] Guid userId)
        {
            return Ok(PlayerService.GetPlayers(userId));
        }

        [HttpGet("getplayerinfo")]
        public IActionResult GetPlayerInfo(Guid playerId) {
            var playerInfo = PlayerService.GetPlayerInfo(playerId);

            if (playerInfo == null) return Ok("Персонаж не найден!");

            return Ok(playerInfo);
        }

        [HttpPost("setplayerinfo")]
        public IActionResult SetPlayerInfo(string playerName, Guid userId) {
            try
            {
                var playerInfo = PlayerService.SetPlayerInfo(playerName, userId);

                if (playerInfo == null) return Ok("Игрок с таким именем уже существует!");

                return Ok(playerInfo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("changeplayername")]
        public IActionResult ChangePlayerName(Guid playerId, string playerName) {
            try
            {
                var result = PlayerService.ChangePlayerName(playerId, playerName);

                if (result == null) return Ok("Данное имя уже используется другим персонажем!");

                return Ok(result);
            }
            catch (Exception ex) { 
                return BadRequest(ex.Message);
            }
        }
    }
}
