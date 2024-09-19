using Core.DatabaseHandler;
using Core.Interfaces;
using Core.Repositories.Interfaces;

namespace Core.Repositories {
    public class UserRepository : Repository<User>, IUserRepository {
        public UserRepository(CoreDbContext context) : base(context) { }
        public CoreDbContext CoreDbContext { get { return Context as CoreDbContext; } }
    }
}
