using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Runtime.Serialization.DataContracts;

namespace Core.Interfaces
{
    [DataContract, Table("inventoryitem")]
    public record InventoryItem([property: Column("id")] Guid Id,  
                                [property: DataMember, Column("itemid")] Guid ItemId,   
                                [property: DataMember, Column("playerid")] Guid PlayerId,                              
                                [property: DataMember, Column("name")] string Name,
                                [property: DataMember, Column("description")] string Description,
                                [property: Column("count")] int Count)
    {
        public PlayerInfo Player { get; init; }
    }
    public interface IInventoryService
    {
        bool TrySpend(int amount, Guid playerId);
        List<InventoryItem> GetInventoryItems(Guid playerId);
        InventoryItem? SpendInventoryItem(Guid itemId, Guid playerId);
        InventoryItem AddItem(StoreItem item, Guid playerId);
    }
}
