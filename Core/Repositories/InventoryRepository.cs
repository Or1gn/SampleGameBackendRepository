using Core.DatabaseHandler;
using Core.Interfaces;
using Core.Repositories.Interfaces;

namespace Core.Repositories
{
    public class InventoryRepository : Repository<InventoryItem>, IInventoryRepository {
        public InventoryRepository(CoreDbContext context) : base(context) { }   

        public CoreDbContext CoreDbContext { get { return Context as CoreDbContext; } }
    }
}
