namespace Core.Core {
    public interface IUnitOfWork : IDisposable{
        void Complete();
    }
}
