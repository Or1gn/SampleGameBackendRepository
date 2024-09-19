using Core.Interfaces;
using static Core.DTO.EntitiesDTO;

namespace Core.Extentions
{
    public static class EntitiesExtentions
    {
        public static EnemyDto AsDto(this Enemy enemy) { 
            return new EnemyDto(enemy.EnemyId, enemy.name, enemy.prehistory);
        }

        public static UserDto AsDto(this User user) {
            return new UserDto(user.id, user.name);
        }

        public static InventroryItemDto AsDto(this InventoryItem inventoryItem) {
            return new InventroryItemDto(inventoryItem.Id, inventoryItem.Name, inventoryItem.Description, inventoryItem.Count);
        }

        public static PlayerInfoDto AsDto(this PlayerInfo playerInfo) {
            return new PlayerInfoDto(playerInfo.Id,
                                        playerInfo.PlayerName, 
                                        playerInfo.Damage, 
                                        playerInfo.Health, 
                                        playerInfo.Armor, 
                                        playerInfo.Gold, 
                                        playerInfo.PlayerLevel);
        }

        public static StoreItemDto AsDto(this StoreItem storeItem) {
            return new StoreItemDto(storeItem.Id, storeItem.Name, storeItem.Description, storeItem.Price);
        }
    }
}
