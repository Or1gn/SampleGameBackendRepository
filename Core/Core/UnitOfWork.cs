using Core.DatabaseHandler;

namespace Core.Core {
    public class UnitOfWork : IUnitOfWork {
        private readonly CoreDbContext context;
        public UnitOfWork(CoreDbContext context) { 
            this.context = context;
        }

        public void Complete() {
            context.SaveChanges();
        }

        public void Dispose() {
            context.Dispose();
        }
    }
}
