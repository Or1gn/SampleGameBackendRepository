using Core.DatabaseHandler;
using Core.Interfaces;
using Core.Repositories.Interfaces;

namespace Core.Repositories {
    public class EnemyRepository : Repository<Enemy>, IEnemyRepository {
        public EnemyRepository(CoreDbContext context) : base(context) { }
        public CoreDbContext CoreDbContext { get { return Context as CoreDbContext;  } }
    }
}
