using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Or1gn_RPG.Controllers {

    [ApiController]
    [Route("api/store")]
    public class StoreController : ControllerBase {
        public IStoreService StoreService { get; }

        public StoreController(IStoreService storeService) {
            StoreService = storeService;
        }

        [HttpGet("getstoreitems")]
        public IActionResult GetStoreItems() {
            return Ok(StoreService.GetStoreItems());
        }

        [HttpGet("purchasestoreitem")]
        public IActionResult PurchaseStoreItem([Required] Guid storeItemId, [Required] Guid playerId) {
            try {
                var storeItem = StoreService.PurchaseStoreItem(storeItemId, playerId);

                if (storeItem == null) return Ok("Не достаточно денег!");

                return Ok(storeItem);
            }
            catch (Exception ex) { 
                return BadRequest(ex.Message);
            }
        }
    }
}
