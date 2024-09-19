using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Runtime.Serialization.DataContracts;
using static Core.DTO.EntitiesDTO;

namespace Core.Interfaces
{
    [DataContract, Table("player")]
    public record PlayerInfo([property: Key, DataMember, Column("id")] Guid Id,
                             [property: DataMember, Column("name")] string PlayerName,
                             [property: DataMember, Column("damage")] int Damage,
                             [property: DataMember, Column("health")] int Health,
                             [property: DataMember, Column("armor")] int Armor,
                             [property: Column("gold")] int Gold,
                             [property: DataMember, Column("userid")] Guid UserId)
    {
        public User User { get; set; }

        [DataMember, Column("level")]
        public int PlayerLevel { get; set; }
        public ICollection<InventoryItem> InventoryItems { get; init; }
    }

    public interface IPlayerInfoService
    {
        IEnumerable<PlayerInfoDto> GetPlayers(Guid userId);
        PlayerInfoDto? GetPlayerInfo(Guid playerId);
        PlayerInfoDto SetPlayerInfo(string playerName, Guid userId);
        PlayerInfoDto ChangePlayerName(Guid playerId, string newPlayerName);
    }
}
