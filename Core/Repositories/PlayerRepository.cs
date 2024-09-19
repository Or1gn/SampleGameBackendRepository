using Core.DatabaseHandler;
using Core.Interfaces;
using Core.Repositories.Interfaces;

namespace Core.Repositories {
    public class PlayerRepository : Repository<PlayerInfo>, IPlayerRepository{
        public PlayerRepository(CoreDbContext context) : base(context) { }  
        public CoreDbContext CoreDbContext { get { return Context as CoreDbContext; } }
    }
}
