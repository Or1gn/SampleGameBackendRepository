using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Core.Interfaces
{
    [DataContract, Table("enemies")]
    public record Enemy([property: Key, DataMember, Column("id")] Guid EnemyId,
                        [property: Column("name")] string name,
                        [property: Column("prehistory")] string prehistory,
                        [property: DataMember, Column("health")] int Health, 
                        [property: DataMember, Column("damage")] int Damage, 
                        [property: DataMember, Column("armor")] int Armor,
                        [property: Column("reward")] int Reward);
    public interface IBattleService
    {
        List<Enemy> GetEnemies();
        Enemy? GetEnemy(Guid enemyId);
        bool StartFigth(Guid enemyId, Guid palyerId);
    }
}
