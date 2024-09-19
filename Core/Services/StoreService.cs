using Core.Interfaces;
using Core.Repositories.Interfaces;

namespace Core.Services
{
    public class StoreService : IStoreService
    {
        private IStoreRepository storeRepository;
        private IInventoryService inventoryService;
        public StoreService(IStoreRepository storeRepository, IInventoryService inventoryService) {
            this.storeRepository = storeRepository;
            this.inventoryService = inventoryService;
        }

        List<StoreItem> IStoreService.GetStoreItems()
        {
            return storeRepository.GetAll().ToList();
        }

        /// <summary>
        /// Возвращает предмет, если его можно купить и null если не хватает средств
        /// </summary>
        /// <param name="storeItemId"></param>
        /// <param name="playerId"></param>
        /// <returns></returns>
        StoreItem IStoreService.PurchaseStoreItem(Guid storeItemId, Guid playerId) {
            var storeItem = storeRepository.Get(storeItemId);

            if (storeItem == null) throw new ArgumentNullException("Предмет в магазине не найден!");
            if (!inventoryService.TrySpend(storeItem.Price, playerId)) throw new Exception("Недостаточно средств!");

            inventoryService.AddItem(storeItem, playerId);
            return storeItem;
        }
    }
}
