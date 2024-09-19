using Core.Core;
using Core.Exceptions.InventoryServiceExceptions;
using Core.Interfaces;
using Core.Repositories.Interfaces;

namespace Core.Services {
    public class InventoryService : IInventoryService {
        public IPlayerRepository PlayerRepository { get; }

        public IInventoryRepository InventoryRepository { get; }

        public IUnitOfWork UnitOfWork { get; }

        public InventoryService(IInventoryRepository inventoryRepository, 
                                IPlayerRepository playerRepository, 
                                IUnitOfWork unitOfWork)
        {
            InventoryRepository = inventoryRepository;
            PlayerRepository = playerRepository;
            UnitOfWork = unitOfWork;
        }
        public List<InventoryItem> GetInventoryItems(Guid playerId) {
            return InventoryRepository.GetAll().ToList();
        }

        public InventoryItem? SpendInventoryItem(Guid itemId, Guid playerId) {
            var inventoryItem = InventoryRepository.Find(x => x.ItemId == itemId && x.PlayerId == playerId).FirstOrDefault();

            if (inventoryItem == null) throw new ArgumentNullException("Предмет не найден!");
            if (inventoryItem.Count == 0) throw new InventoryItemCountIsZeroException(inventoryItem.Name);

            int increasedGoldCount = inventoryItem.Count - 1;

            var updatedInventoryItem = inventoryItem with { Count = increasedGoldCount };
            InventoryRepository.Update(updatedInventoryItem);

            UnitOfWork.Complete();

            return updatedInventoryItem;
        }

        public bool TrySpend(int amount, Guid playerId)
        {
            var playerInfo = PlayerRepository.Get(playerId);
            if(playerInfo == null) throw new ArgumentNullException("Игрок не найден!");

            if (playerInfo.Gold < amount) throw new Exception("Недостаточно золота!");

            int finalGoldValue = playerInfo.Gold - amount;

            var updatedPlayerInfo = playerInfo with { Gold = finalGoldValue };
            PlayerRepository.Update(updatedPlayerInfo);
            UnitOfWork.Complete();

            return true;
        }

        public InventoryItem AddItem(StoreItem item, Guid playerId)
        {
            var inventoryItem = InventoryRepository.Find(x => x.ItemId == item.Id && x.PlayerId == playerId).FirstOrDefault();
            bool isExist = inventoryItem != null;

            if (!isExist) {
                inventoryItem = new InventoryItem(Guid.NewGuid(), item.Id, playerId, item.Name, item.Description, 0);
            }

            int increasedGoldCount = inventoryItem.Count + 1;

            var updatedInventoryItem = inventoryItem with { Count = increasedGoldCount };
            if (isExist) {
                InventoryRepository.Update(updatedInventoryItem);
            }
            else {
                InventoryRepository.Add(updatedInventoryItem);
            }

            UnitOfWork.Complete();

            return updatedInventoryItem;
        }
    }
}
