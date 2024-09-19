using Core.DatabaseHandler;
using Core.Interfaces;
using Core.Repositories.Interfaces;

namespace Core.Repositories
{
    public class StoreRepository : Repository<StoreItem>, IStoreRepository
    {
        public StoreRepository(CoreDbContext context) : base(context) { }

        public CoreDbContext CoreDbContext { get { return Context as CoreDbContext; } }
    }
}
