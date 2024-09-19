using Core.Extentions;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Or1gn_RPG.Controllers
{
    [ApiController]
    [Route("api/battle")]
    public class BattleController : ControllerBase
    {
        public IBattleService BattleService { get; }
        public BattleController(IBattleService battleService) {
            BattleService = battleService;
        }

        [HttpGet("getenemies")]
        public IActionResult GetEnemies() {
            return Ok(BattleService.GetEnemies().Select(x => x.AsDto()).ToList());
        }

        [HttpGet("getenemy/{id}")]
        public IActionResult GetEnemy(Guid id) {
            return Ok(BattleService.GetEnemy(id)?.AsDto());
        }

        [HttpGet("fight/{enemyId}/{playerId}")]
        public IActionResult FigthEnemy(Guid enemyId, Guid playerId) {
            return Ok(BattleService.StartFigth(enemyId, playerId));
        }
    }
}
