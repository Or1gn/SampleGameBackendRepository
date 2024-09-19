using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Core.Interfaces
{
    [DataContract, Table("storeitem")]
    public record StoreItem([property: Key, DataMember, Column("id")] Guid Id,
                            [property: DataMember, Column("name")] string Name,
                            [property: DataMember, Column("description")] string Description,
                            [property: Column("price")] int Price);
    public interface IStoreService
    {
        List<StoreItem> GetStoreItems();
        StoreItem PurchaseStoreItem(Guid storeItem, Guid playerId);
    }
}
