namespace Core.DTO
{
    public class EntitiesDTO
    {
        public record EnemyDto(Guid id, string name, string prehistory);
        public record UserDto(Guid id, string name);
        public record InventroryItemDto(Guid id, string name, string description, int count);    
        public record PlayerInfoDto(Guid id, string playerName, int damage, int health, int armor, int gold, int playerLevel);
        public record StoreItemDto(Guid id, string name, string description, int price);
        public record ErrorDto(string message);
    }
}
