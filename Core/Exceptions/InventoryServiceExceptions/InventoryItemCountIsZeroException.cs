namespace Core.Exceptions.InventoryServiceExceptions
{
    public class InventoryItemCountIsZeroException : Exception
    {
        public InventoryItemCountIsZeroException(string itemName) : base($"Количество {itemName} равно нулю!") { }
    }
}
