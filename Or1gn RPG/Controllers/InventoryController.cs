using Core.Extentions;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Or1gn_RPG.Controllers {

    [ApiController]
    [Route("api/inventory")]
    public class InventoryController : ControllerBase {
        public IInventoryService InventoryService { get; }
        public InventoryController(IInventoryService inventoryService) {
            InventoryService = inventoryService;
        }

        [HttpGet("getinventoryitems")]
        public IActionResult GetInventoryItems(Guid playerId) {
            try {
                return Ok(InventoryService.GetInventoryItems(playerId).Select(x => x.AsDto()).ToList());
            }
            catch (Exception ex) { 
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("spendinventoryitem")]
        public IActionResult SpendInventoryItem(Guid itemId, Guid playerId) {
            try
            {
                var result = InventoryService.SpendInventoryItem(itemId, playerId);

                if (result == null) return Ok("Предмет не найден!");

                return Ok(result.AsDto());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
